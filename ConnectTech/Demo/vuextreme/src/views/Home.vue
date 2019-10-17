<template>
  <div class="row">
    <div class="col-12">
      <h2>Regions</h2>
      <div class="alert alert-warning" v-if="error">{{ error }}</div>
      <div class="table-responsive">
        <table class="table col-12 text-left w-100">
          <thead>
            <tr>
              <th>Name</th>
            </tr>
          </thead>
          <tbody v-for="r in regions" :key="r.key">
            <tr>
              <td><router-link :to="{ name: 'region', params: { region: r } }">{{ r.name }}</router-link></td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import { mapActions, mapState } from "vuex"; 

export default {
  name: "home",
  computed: mapState(["error", "regions"]),
  async mounted() {
    await this.loadRegions();
  },
  methods: {
   ...mapActions(["loadRegions"]) 
  }
};
</script>

