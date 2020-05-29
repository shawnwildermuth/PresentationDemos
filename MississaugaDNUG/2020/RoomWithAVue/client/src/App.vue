<template>
  <div class="row">
    <div class="col-md-12"><wait-cursor :busy="isBusy" msg="Please Wait..."></wait-cursor></div>
    <div v-if="selected" class="col-md-12">Selected: {{ selected }}</div>
    <div class="col-md-4" v-for="r in rooms" v-if="!isBusy">
      <div class="card">
        <img class="card-img" v-bind:src="r.images[0].url" />
        <div class="card-body">
          <h4 class="card-title">{{ r.title }}</h4>
          <p class="card-text">{{ r.description }}</p>
          <ul class="list-unstyled">
            <li><strong><i class="fas fa-male"></i> Max Guests:</strong> {{ r.maxGuests }}</li>
            <li><strong><i class="fas fa-money-bill-alt"></i> Price:</strong> ${{ r.rate }}</li>
          </ul>
        </div>
        <div class="card-footer">
          <div class="float-right">
            <button class="btn btn-sm btn-success" @click="onBook(r)">Book Now</button>
          </div>
        </div>
      </div>
    </div>

  </div>
</template>

<script>
  import axios from 'axios';
  import WaitCursor from "./components/waitcursor";
  export default {
    data: () => {
      return {
        rooms: [],
        selected: "",
        isBusy: false
      }
    },
    mounted: async function () {
      try {
        this.isBusy = true;
        var result = await axios.get("/api/rooms");
        if (result.statusCode == 200) {
          this.rooms = result.data;
        } //...
      } finally {
        this.isBusy = false;
      } 
    },
    components: { WaitCursor },
    methods: {
      onBook(room) {
        this.selected = room.title;
      }
    }
  };
</script>

