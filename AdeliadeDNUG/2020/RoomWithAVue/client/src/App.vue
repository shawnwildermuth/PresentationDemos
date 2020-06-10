<template>
  <div>
    <wait-cursor msg="Loading..." :busy="isBusy"></wait-cursor>
    <div class="row">
      <div v-for="r in rooms" class="col-4">
        <div class="card">
          <img class="card-img" :src="r.images[0].url" />
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
              <button class="btn btn-sm btn-success" @click="bookRoom(r)">Book Now</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import axios from "axios";
  import WaitCursor from "./components/waitCursor";

  export default {
    data: () => {
      return {
        rooms: [],
        isBusy: false
      }
    },
    methods: {
      bookRoom(room) {
        alert(room.title);
      }
    },
    components: {
      WaitCursor
    },
    async mounted() {
      try {
        this.isBusy = true;
        let result = await axios.get("/api/rooms");
        this.rooms = result.data;
      } finally {
        this.isBusy = false;
      }
    }
  };
</script>

