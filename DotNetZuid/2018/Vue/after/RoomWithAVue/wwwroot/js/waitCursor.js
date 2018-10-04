Vue.component("wait-cursor", {
  template: `<div class="alert alert-info" v-if="busy">{{ msg }}</div>`,
  props: {
    msg: String,
    busy: Boolean
  }
});