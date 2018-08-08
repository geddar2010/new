<template>
        <b-navbar toggleable="md" type="dark" variant="info">
            <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>
            <b-navbar-brand :to="{path:'/'}">Домой</b-navbar-brand>
            <b-collapse is-nav id="nav_collapse">
                <!--<b-navbar-nav>
                    <b-nav-item :to="{path:'/'}">Домой</b-nav-item>
                </b-navbar-nav>-->
            </b-collapse>
        </b-navbar>
</template>

<script>
import { routes } from "../routes";

export default {
  data() {
    return {
      routes,
      collapsed: true
    };
  },
  methods: {
    toggleCollapsed: function(event) {
      this.collapsed = !this.collapsed;
    }
  }
};
</script>

<style>
.slide-enter-active,
.slide-leave-active {
  transition: max-height 0.35s;
}
.slide-enter,
.slide-leave-to {
  max-height: 0px;
}

.slide-enter-to,
.slide-leave {
  max-height: 20em;
}
</style>
