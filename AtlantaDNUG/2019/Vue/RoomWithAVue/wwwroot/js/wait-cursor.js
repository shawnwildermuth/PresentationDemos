// wait-cursor.js
Vue.component("wait-cursor", {
  template: `<div v-if="busy" class="alert alert-danger">Please Wait...{{ msg }}</div>`,
  props: {
    busy: Boolean,
    msg: String
  }
});