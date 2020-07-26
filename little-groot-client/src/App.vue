<template>
  <div id="little-groot">
    <router-view />
  </div>
</template>

<script lang="ts">
export default {
  name: 'Home',
  components: {}
};
</script>

<style lang="scss">
body {
  margin: 0;
}
#little-groot {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;

  height: 100vh;
  width: 100vw;
}
</style>
