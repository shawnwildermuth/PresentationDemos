// app.js
var vm = new Vue({
  el: "#theApp",
  data: {
    name: "Shawn",
    rooms: [
    ]

  },
  mounted() {
    axios.get("/api/rooms")
      .then((res) => {
        this.rooms = res.data;
      });
  },
  methods: {
    onAdd() {
      this.rooms.push({ title: "New Room" });
    },
    onBook(room) {
      // Do SOmething Amazing
      alert(room.title);
    }
  }
});