<template>
    <div id="app" class="container-fluid">
        <div class="row-fluid">
            <nav-menu params="route: route"></nav-menu>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <router-view></router-view>
            </div>
        </div>
    </div>
</template>

<script>
import Vue from "vue";
import GameStats from "./game-stats";
import HomePage from "./home-page";
import NavMenu from "./nav-menu";

Vue.component("game-stats", GameStats);
Vue.component("home-page", HomePage);
Vue.component("nav-menu", NavMenu);

export default {
  data() {
    return {};
  }
};
</script>

<style>
</style>
