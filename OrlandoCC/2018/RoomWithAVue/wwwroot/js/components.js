Vue.component("wait-cursor", {
  template: "<div class='alert alert-info' v-if='busy' ><i class='fas fa-cog fa-spin'></i> {{ msg }}</div>",
  props: [
    "msg",
    "busy"
  ]
});