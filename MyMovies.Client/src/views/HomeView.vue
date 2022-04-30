<template>
  <movies-list v-on="$listeners" />
</template>

<script>
import MoviesList from "@/components/MoviesList.vue";

export default {
  name: "Home-view",
  components: {
    MoviesList,
  },
};
</script>
