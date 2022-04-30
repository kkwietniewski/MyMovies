<template>
  <v-container>
    <v-data-table
      :headers="headers"
      :items="movies"
      :items-per-page="-1"
      class="elevation-1"
      hide-default-header
      hide-default-footer
    >
      <template v-slot:header="{ props }">
        <th v-for="head in props.headers" :key="head.value">
          <h4 class="headers">{{ head.text }}</h4>
          <div v-if="head.value == 'actions'">
            <v-dialog v-model="addDialog" persistent max-width="600px">
              <template v-slot:activator="{ on, attrs }">
                <v-btn dark small color="green" v-bind="attrs" v-on="on">
                  <v-icon dark>mdi-plus</v-icon>
                </v-btn>
              </template>
              <movie-item
                :getMovies="getMovies"
                v-on="$listeners"
                @close-add-dialog="addDialog = false"
              />
            </v-dialog>
          </div>
        </th>
      </template>

      <template v-slot:item="row">
        <tr>
          <td>
            <v-btn
              class="mx-1"
              fab
              dark
              small
              color="red"
              @click="handleBtnDelete(row.item)"
            >
              <v-icon dark>mdi-close-thick</v-icon>
            </v-btn>
          </td>
          <td>{{ row.item.title }}</td>
          <td>{{ row.item.productionYear }}</td>
          <td>
            <v-dialog
              v-model="editDialogs[row.item.id]"
              persistent
              max-width="600px"
            >
              <template v-slot:activator="{ on, attrs }">
                <v-btn fab dark small color="blue" v-bind="attrs" v-on="on">
                  <v-icon dark>mdi-pencil</v-icon>
                </v-btn>
              </template>
              <movie-item
                :getMovies="getMovies"
                :item="row.item"
                v-on="$listeners"
                @close-edit-dialog="editDialogs[row.item.id] = false"
              />
            </v-dialog>
          </td>
          <td>
            <v-dialog
              v-model="viewDialogs[row.item.id]"
              persistent
              max-width="600px"
            >
              <template v-slot:activator="{ on, attrs }">
                <v-btn fab dark small color="gray" v-bind="attrs" v-on="on">
                  <v-icon dark>mdi-eye</v-icon>
                </v-btn>
              </template>
              <movie-item
                :item="row.item"
                :view_mode="true"
                v-on="$listeners"
                @close-edit-dialog="viewDialogs[row.item.id] = false"
              />
            </v-dialog>
          </td>
        </tr>
      </template>
    </v-data-table>
  </v-container>
</template>

<script>
import useMovies from "@/modules/movies";
import MovieItem from "@/components/MovieItem.vue";
export default {
  name: "MoviesList",
  components: {
    MovieItem,
  },
  data() {
    return {
      headers: [
        { text: "", value: "" },
        { text: "Title", value: "title" },
        { text: "Production Year", value: "productionYear" },
        { text: "", value: "sep" },
        { text: "", value: "sep1" },
        { text: "", value: "actions" },
      ],
      movies: [],
      addDialog: null,
      editDialogs: [],
      viewDialogs: [],
    };
  },
  methods: {
    handleBtnDelete(item) {
      if (confirm("Are you sure you want to delete movie?")) {
        this.removeMovie(item.id);
      }
    },
    handleBtnEdit(item) {
      this.editMovie(item);
    },
  },
  created() {
    this.getMovies();
  },
  setup() {
    const { getAllMovies, deleteMovie } = useMovies();

    const getMovies = function () {
      getAllMovies().then((response) => {
        this.movies = response;
      });
    };

    const removeMovie = function (id) {
      deleteMovie(id).then(
        (response) => {
          if (response.status == 200) {
            this.getMovies();
          } else {
            this.$emit(
              "show-alert",
              `Something went wrong. ${response.status} ${response.message}`,
              "error"
            );
          }
        },
        (error) => {
          this.$emit(
            "show-alert",
            `Something went wrong. ${error.message}`,
            "error"
          );
        }
      );
    };

    return {
      getMovies,
      removeMovie,
    };
  },
};
</script>

<style scoped>
.headers {
  text-align: left;
  padding-left: 4%;
}
</style>
