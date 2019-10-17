<template>
  <div class="m-1 border site-block">
    <button @click="onToggle()" class="btn btn-sm btn-secondary float-right">
      <i :class="icon"></i>
    </button>
    <h5 v-html="site.name" @click="onToggle()"></h5>
    <div v-show="show" class="col-12">
      <a :href="site.url" target="_blank">
        <img :src="site.imageUrl" class="img-thumbnail img-fluid w-25 float-right" />
      </a>
      <div v-html="site.descriptionMarkup"></div>
      <div class="row">
        <div class="col-9">
          <table>
            <tr>
              <td>Year Inscribed:</td>
              <td class="bold">{{ site.yearInscribed }}</td>
            </tr>
            <tr>
              <td>Location:</td>
              <td class="bold">{{ site.location.name }} - {{ site.states }}</td>
            </tr>
            <tr>
              <td>Category:</td>
              <td class="bold">{{ site.categoryName }}</td>
            </tr>
          </table>
        </div>
        <div class="col-3">
          <a :href="site.url" target="_blank" class="btn btn-success btn-sm m-1 float-right">More Info...</a>
          <br/>
          <button class="btn btn-success btn-sm m-1 float-right" @click="addToCart(site)">Add to Cart</button>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { mapMutations } from "vuex";

export default {
  data() {
    return {
      show: false,
      icon: "fas fa-chevron-down"
    };
  },
  props: ["site"],
  methods: {
    onToggle() {
      this.$store.state.isBusy = true;
      this.show = !this.show;
      if (this.show) {
        this.icon = "fas fa-chevron-up";
      } else {
        this.icon = "fas fa-chevron-down";
      }
    },
    ...mapMutations(["addToCart"])
  }
};
</script>
<style>
.site-block {
  min-height: 32px;
}
.bold {
  font-weight: 600;
}
</style>