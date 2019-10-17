<template>
  <div class="row">
    <div class="col-12" v-if="region">
      <router-link class="btn btn-sm btn-primary" to="/"><i class="fas fa-arrow-left"></i> Back</router-link>
      <h3>{{ region.name }}</h3>
      <div v-for="s in currentSites" :key="s.id">
        <Site :site="s"></Site>
      </div>
    </div>
  </div>
</template>
<script>
import Site from "@/components/Site.vue";
import { mapActions, mapState } from "vuex";

export default {
  computed: mapState(["currentSites"]),
  props: [
    "region"
  ],
  components: { Site },
  async created() {
    if (!this.region) return this.$router.push("/");
    await this.loadSites(this.region);
  },
  methods: {
    ...mapActions(["loadSites"])
  }
}</script>