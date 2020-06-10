<template>
  <div>
    <div class="alert alert-warning" v-if="busy">
      <i class="fa-spinner"></i> {{ msg }} </div>
  </div>
</template>

<script>
  export default {
    props: [
      "msg",
      "busy"
    ]
  }
</script>