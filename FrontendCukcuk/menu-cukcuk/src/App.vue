<template>
  <div id="app">
    <div id="header">
      <the-header/>
    </div>
    <div class="wrap">
      <div id="navbar">
        <the-navbar/>
      </div>
      <div id="content">
        <router-view/>
      </div>
    </div>
  </div>
</template>

<script>
import TheHeader from './components/layout/TheHeader.vue'
import TheNavbar from './components/layout/TheNavbar.vue'
export default {
  components: { TheHeader, TheNavbar },
  
}
</script>

<style>
@import url('./assets/css/main.css');
</style>
