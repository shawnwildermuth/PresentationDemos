<template>
  <div class="container">
    <div class="row">
      <div class="alert alert-secondary" v-if="isBusy">Loading...</div>
      <div class="alert alert-warning" v-if="error">{{ error }}</div>
    </div>
    <div class="row">
      <div class="col-9">
        <div id="app">
          <router-view />
        </div>
      </div>
      <div class="col-3">
        <Cart></Cart>
      </div>
    </div>
  </div>
</template>
<script>
import Cart from "@/components/Cart.vue";
import { mapState } from "vuex";

export default {
  computed: mapState(["isBusy", "error"]),
  components: {
    Cart
  }
}
</script>