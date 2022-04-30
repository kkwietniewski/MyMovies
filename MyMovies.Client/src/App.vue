<template>
  <v-app>
    <v-main>
      <v-snackbar v-model="alert_show" :timeout="timeout">
        {{ alert_message }}
        <template v-slot:action="{ attrs }">
          <v-btn
            :color="alert_type"
            text
            v-bind="attrs"
            @click="alert_show = false"
          >
            Zamknij
          </v-btn>
        </template>
      </v-snackbar>
      <router-view @show-alert="showAlert" />
    </v-main>
  </v-app>
</template>

<script>
export default {
  name: "App",
  data() {
    return {
      alert_message: "",
      alert_show: false,
      timeout: 2500,
      alert_type: "",
    };
  },
  methods: {
    showAlert(message, type) {
      this.alert_show = true;
      this.alert_message = message;
      this.alert_type = type;
    },
  },
};
</script>
