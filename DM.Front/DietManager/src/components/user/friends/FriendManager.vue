<template>
  <side-menu>
    <router-link tag="li" class="link" :to="{ name: 'NewsFeed'}">
      <font-awesome-icon id="news-feed-icon" class="option-icon" icon="newspaper"/>News Feed
    </router-link>

    <router-link tag="li" class="link" :to="{ name: 'SearchUsers'}">
      <font-awesome-icon id="search-friends-icon" class="option-icon" icon="search"/>Search
    </router-link>

    <router-link tag="li" class="link" :to="{ name: 'MyFriends'}">
      <font-awesome-icon id="my-friends-icon" class="option-icon" icon="user-friends"/>My Friends
    </router-link>

    <router-link tag="li" class="link" :to="{ name: 'FriendInvitations'}">
      <font-awesome-icon id="friend-invitations-icon" class="option-icon" icon="envelope"/>Invitations
    </router-link>
  </side-menu>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import SideMenu from "@/components/common/SideMenu.vue";

@Component({
  components: {
    "side-menu": SideMenu
  }
})
export default class MealsManager extends Vue {}
</script>

<style lang="less" scoped>
#news-feed-icon {
  position: relative;
  right: 5px;
}
#search-friends-icon {
  position: relative;
  right: 22px;
}
#my-friends-icon {
  position: relative;
  right: 7px;
}
#friend-invitations-icon {
  position: relative;
  right: 12px;
}
</style>
