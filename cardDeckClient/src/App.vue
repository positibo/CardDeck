<template>
  <div id="app">
    <DeckComponent/>
  </div>
</template>

<script>
import DeckComponent from "../src/components/deck.component";

export default {
  name: 'app',
  components: {
    DeckComponent
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
