var WaitCursor = Vue.component("wait-cursor", {
  template: `
  <div v-if="busy" class="alert alert-info"><i class="fas fa-spinner fa-spin"></i> {{ msg }}</div>
`,
  props: {
    msg: {
      type: String,
      isRequired: true
    },
    busy: {
      type: Boolean
    }
  }
});