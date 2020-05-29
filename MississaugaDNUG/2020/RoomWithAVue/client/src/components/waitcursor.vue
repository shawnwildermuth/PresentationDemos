<template>
  <div class="alert alert-info" v-if="busy">
    <i class="fas fa-cog fa-spin"></i> {{ msg }}
  </div>
</template>
<script>
  export default {
    props: {
      msg: String,
      busy: Boolean
    }
  }
</script>