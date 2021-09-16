TT<template>
<div>Heloo</div>
<Teleport to='#ourMenu'>
  <div @click="onClick">{{ uppercase(name) }}</div>
</Teleport>
</template>

<script lang="ts">
import { ref, computed, reactive, defineComponent } from "vue";
//import filters from "../filters";

export default defineComponent({
  props: {
    id: {
      required: true
    }
  },
  setup(props, opt) {

    opt.

   const name = ref("Shawn");
   const items = reactive([]);
    function onClick() {
      name.value = "Bob";
    }

   const nameLength = computed(() => name.value.length);

   function uppercase(val: String) {
     return val.toUpperCase();
   } 
     return {
     name,
     nameLength,
     items,
     onClick,
     uppercase,
//     ...filters
    }
  }
});
</script>


