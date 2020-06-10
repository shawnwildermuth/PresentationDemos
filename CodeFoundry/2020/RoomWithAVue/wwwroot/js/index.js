// index.js
new Vue({
  data: {
    name: "Shawn",
    rooms: []
  },
  mounted() {
    axios.get("/api/rooms")
      .then(result => {
        this.rooms = result.data;
      });
  },
  methods: {
    book(room) {
      alert(room.title);
    },
    addNew() {
      this.rooms.push({
        title: "A new room",
        images: [
           "/img/varsity-1.jpg"
        ]
      })
    }
  }
}).$mount("#theApp");