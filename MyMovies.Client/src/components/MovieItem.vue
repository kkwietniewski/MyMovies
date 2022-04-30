<template>
  <v-card>
    <v-card-title>
      <span v-if="view_mode != true" class="text-h5">{{
        item != null ? "Edit Movie" : "Add Movie"
      }}</span>
      <span v-else class="text-h5">View Movie</span>
    </v-card-title>
    <v-card-text>
      <v-container>
        <v-row>
          <v-col cols="12" sm="6">
            <v-text-field
              label="Title*"
              prepend-icon="mdi-book-open-variant"
              v-model.trim="$v.movie.title.$model"
              :error-messages="titleErrors"
              :counter="200"
              required
              :disabled="view_mode == true"
              @input="$v.movie.title.$touch()"
              @blur="$v.movie.title.$touch()"
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6">
            <v-text-field
              label="Production Year"
              prepend-icon="mdi-calendar-text"
              v-model.trim="$v.movie.productionYear.$model"
              :error-messages="productionYearErrors"
              :counter="4"
              :disabled="view_mode == true"
              @input="$v.movie.productionYear.$touch()"
              @blur="$v.movie.productionYear.$touch()"
            ></v-text-field>
          </v-col>
        </v-row>
      </v-container>
      <small v-if="view_mode != true">*indicates required field</small>
    </v-card-text>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="blue darken-1" text @click="handleBtnClose"> Close </v-btn>
      <v-btn
        v-if="item && view_mode != true"
        color="blue darken-1"
        :disabled="$v.movie.$invalid"
        text
        @click="handleBtnEdit"
      >
        Save </v-btn
      ><v-btn
        v-else-if="view_mode != true"
        color="blue darken-1"
        :disabled="$v.movie.$invalid"
        text
        @click="handleBtnAdd"
      >
        Save
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import {
  required,
  minLength,
  maxLength,
  maxValue,
  minValue,
  numeric,
} from "vuelidate/lib/validators";
import useMovies from "@/modules/movies";
import Movie from "@/models/Movie";
export default {
  name: "MovieItem",
  data() {
    return {
      movie: new Movie(),
    };
  },
  props: {
    getMovies: Function,
    item: Object,
    view_mode: Boolean,
  },
  validations: {
    movie: {
      title: {
        required,
        maxLength: maxLength(200),
      },
      productionYear: {
        minLength: minLength(4),
        maxLength: maxLength(4),
        minValue: minValue(1900),
        maxValue: maxValue(2100),
        numeric,
      },
    },
  },
  computed: {
    isDisabled() {
      return this.$v.$invalid;
    },
    titleErrors() {
      return this.prepareErrorArray("title");
    },
    productionYearErrors() {
      return this.prepareErrorArray("productionYear");
    },
  },
  created() {
    if (this.item != null) {
      this.movie = this.item;
    }
  },
  methods: {
    handleBtnAdd() {
      this.handleAddMovie();
    },
    handleBtnEdit() {
      this.editMovie();
    },
    handleBtnClose() {
      if (this.item != null) {
        this.$emit("close-edit-dialog");
      } else {
        this.$emit("close-add-dialog");
      }
    },
    prepareErrorArray(field) {
      const errors = [];
      if (!this.$v.movie[field].$dirty) return errors;
      if (this.$v.movie[field].required != undefined) {
        !this.$v.movie[field].required && errors.push("Field is required.");
      }
      if (this.$v.movie[field].maxLength != undefined) {
        !this.$v.movie[field].maxLength &&
          errors.push(
            `Field cannot be longer than ${this.$v.movie[field].$params.maxLength.max} characters.`
          );
      }
      if (this.$v.movie[field].minLength != undefined) {
        !this.$v.movie[field].minLength &&
          errors.push(
            `Field must contain at least ${this.$v.movie[field].$params.minLength.min} characters.`
          );
      }
      if (this.$v.movie[field].numeric != undefined) {
        !this.$v.movie[field].numeric &&
          errors.push(`Field cannot have non numeric value.`);
      }
      if (
        this.$v.movie[field].maxValue != undefined &&
        this.$v.movie[field].minValue != undefined
      ) {
        !this.$v.movie[field].maxValue &&
          errors.push(
            `Field value must be lower than ${this.$v.movie[field].$params.maxValue.max}.`
          );
        !this.$v.movie[field].minValue &&
          errors.push(
            `Field value must be higher than ${this.$v.movie[field].$params.minValue.min}.`
          );
      }
      return errors;
    },
  },

  setup() {
    const { addMovie, updateMovie } = useMovies();

    const handleAddMovie = function () {
      delete this.movie.id;
      addMovie(this.movie).then(
        (response) => {
          if (response.status == 200) {
            this.getMovies();
            this.$emit("show-alert", `${this.movie.title} Created.`, "success");
            this.handleBtnClose();
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
            `Something went wrong. ${error.response.data.title}`,
            "error"
          );
        }
      );
    };

    const editMovie = function () {
      updateMovie(this.movie).then(
        (response) => {
          if (response.status == 200) {
            this.getMovies();
            this.$emit("show-alert", `${this.movie.title} Saved.`, "success");
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
      handleAddMovie,
      editMovie,
    };
  },
};
</script>
