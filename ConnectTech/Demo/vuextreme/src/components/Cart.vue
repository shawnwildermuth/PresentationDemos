<template>
  <div class="stick">
    <div class="row">
      <div class="col-12">
        <h4>Cart</h4>
        <div class="border">
          <div>Sites to Visit:</div>
          <div v-for="i in cart" :key="i.id" v-html="i.name"></div>
        </div>
        <button class="btn btn-secondary" @click="clearCart()">Clear</button>
      </div>
    </div>
  </div>
</template>
<script>
import { mapState, mapMutations } from "vuex"; 

export default {
  computed: mapState(["cart"]),
  methods: {
    ...mapMutations(["clearCart"])
  }
}
</script>
<style>
.stick { position: sticky; top: 25px;}</style>