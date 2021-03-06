<template>
  <div id="preview-item" :class="mealPreview.isSelected ? 'meal-selected' : ''">
    <modal v-if="showDeleteModal">
      <span class="modal-text">Are you sure you want to delete this meal?</span>
      <div class="modal-buttons-container">
        <button class="button" @click="deleteMeal">Delete</button>
        <button class="button" @click="showDeleteModal = false">Cancel</button>
      </div>
    </modal>

    <div class="avatar-image-container">
      <image-wrapper :imageId="mealPreview.imageId">
        <template slot="placeholder">
          <font-awesome-icon class="main-color" icon="utensils" size="2x"/>
        </template>
      </image-wrapper>
      <div v-if="mealPreview.isFavourite" class="pin">
        <font-awesome-icon class="main-color" icon="star"/>
      </div>
      <div v-else-if="createdByUser && enableFavouriteMarkToggling" class="pin">
        <font-awesome-icon class="main-color" icon="user-circle"/>
      </div>
    </div>
    <div class="meal-info-element meal-name">
      <div class="label">Name</div>
      <router-link
        :class="emitEvents ? 'meal-link' : ''"
        :to="{ name: 'Meal', params: { mealId: mealPreview.id, asAdmin: isAdmin } }"
        class="value"
      >{{mealPreview.name}}</router-link>
    </div>
    <router-link
      v-if="isAdmin"
      tag="div"
      class="meal-info-element meal-name link"
      :to="{name: 'Friend', params: { userId: mealPreview.creator.id, asAdmin: isAdmin } }"
    >
      <div class="label">Created by</div>
      <div class="value">{{creatorName}}</div>
    </router-link>
    <div v-else class="meal-info-element meal-name">
      <div class="label">Created by</div>
      <div class="value">{{creatorName}}</div>
    </div>
    <div class="meal-info-element">
      <div class="label">Calories</div>
      <div class="value">{{mealPreview.calories}}</div>
    </div>
    <span v-if="!isMobile" class="details">
      <div class="meal-info-element">
        <div class="label">Used</div>
        <div class="value">{{mealPreview.numberOfUses}} times</div>
      </div>
      <div class="meal-info-element">
        <div class="label">Favourite by</div>
        <div class="value">{{mealPreview.numberOfFavouriteMarks}} people</div>
      </div>
    </span>
    <div id="delete-item-button" v-if="isAdmin">
      <button class="button" @click="showDeleteModal = true">Delete</button>
    </div>
    <div id="add-to-favourites-wrapper" v-if="enableFavouriteMarkToggling">
      <div
        v-if="enableFavouriteMarkToggling && mealPreview.isFavourite !== null"
        id="add-to-favourites-button"
        @click="toggleFavouriteMark"
      >
        <div id="favourite-label" class="label">Favourite</div>
        <font-awesome-icon
          v-if="mealPreview.isFavourite === false"
          id="add-icon"
          class="option-icon"
          icon="plus-circle"
        />
        <font-awesome-icon v-else id="delete-icon" class="option-icon" icon="minus-circle"/>
      </div>
    </div>

    <router-link
      v-if="!emitEvents"
      class="go-to-meal"
      :to="{ name: 'Meal', params: { mealId: mealPreview.id, asAdmin: isAdmin } }"
    >
      <font-awesome-icon class="main-color" icon="arrow-alt-circle-right" size="2x"/>
    </router-link>
    <div v-else @click="onMealSelected" id="select-button">
      <font-awesome-icon id="add-icon" class="option-icon" icon="plus-circle"/>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import MealPreview from "@/ViewModels/meal/mealPreview";
import MealApiCaller from "@/services/api-callers/mealApi";
import IndexedResult from "@/ViewModels/wrappers/indexedResult";
import ImageApiCaller from "@/services/api-callers/imageApi";
import FavouritesApiCaller from "@/services/api-callers/favouritesApi";
import { Prop } from "vue-property-decorator";
import ImageWrapper from "@/components/image/ImageWrapper.vue";
import AuthService from "@/services/authService";
import AdminApiCaller from "@/services/api-callers/adminApi";
import Modal from "@/components/common/Modal.vue";

Component.registerHooks(["beforeRouteEnter"]);

@Component({
  components: {
    "image-wrapper": ImageWrapper,
    modal: Modal
  }
})
export default class MealPreviewItem extends Vue {
  @Prop({ required: true })
  private mealPreview!: MealPreview;
  @Prop({ required: true })
  private enableFavouriteMarkToggling!: boolean;
  @Prop({ required: false, default: false })
  private emitEvents!: boolean;
  private showDeleteModal = false;

  get creatorName() {
    if (
      this.mealPreview.creator.name === "" &&
      this.mealPreview.creator.surname === ""
    ) {
      return "account deleted";
    } else {
      return (
        this.mealPreview.creator.name + " " + this.mealPreview.creator.surname
      );
    }
  }

  onMealSelected() {
    this.$emit("mealSelected", this.mealPreview.id);
  }

  get isMobile() {
    if (window.innerWidth < 860) {
      return true;
    } else {
      return false;
    }
  }

  get isAdmin() {
    return this.$route.meta && this.$route.meta.asAdmin;
  }

  deleteMeal() {
    AdminApiCaller.deleteMeal(this.mealPreview.id, () =>
      this.$emit("meal-deleted", this.mealPreview.id)
    );
  }

  get createdByUser() {
    return this.mealPreview.isFavourite === null;
  }

  toggleFavouriteMark() {
    if (this.mealPreview.isFavourite) {
      FavouritesApiCaller.delete(
        this.mealPreview.id,
        this.deletedFromFavouritesSuccessHandler
      );
    } else {
      FavouritesApiCaller.add(
        this.mealPreview.id,
        this.addedToFavouritesSuccessHandler
      );
    }
  }

  deletedFromFavouritesSuccessHandler() {
    this.mealPreview.isFavourite = false;
    this.mealPreview.numberOfFavouriteMarks--;
    this.$emit("deleted-from-favourites", this.mealPreview.id);
  }

  addedToFavouritesSuccessHandler() {
    this.mealPreview.isFavourite = true;
    this.mealPreview.numberOfFavouriteMarks++;
  }
}
</script>

<style lang="less">
#delete-item-button {
  height: 100%;
  margin: auto 35px auto 0px;
}
.link {
  .value {
    text-decoration: underline;
  }
}
#favourite-label {
  position: relative;
  left: -10px;
  top: 5px;
  width: fit-content;
  font-size: 0.9em;
}
.meal-info-element {
  flex-grow: 1;
  margin-left: 10px;
  display: inline-block;
  margin-top: auto;
  margin-bottom: auto;
  width: 100px;
}
.meal-name {
  overflow: hidden;
}
.label {
  color: #929191;
}
.go-to-meal {
  margin-top: auto;
  margin-bottom: auto;
  text-align: center;
  width: 65px;
}
.go-to-meal:hover {
  position: relative;
  animation-name: button-animation;
  animation-duration: 0.1s;
  animation-timing-function: ease-in-out;
  animation-fill-mode: both;
}
.details {
  flex-grow: 2;
  display: flex;
  justify-content: space-between;
}
.details > * {
  width: 100px;
}
#add-to-favourites-wrapper {
  position: relative;
  width: 60px;
}
#add-to-favourites-button {
  position: relative;
  top: -8px;
  padding: 5px;
  width: 50px;
}

.meal-link {
  text-decoration: underline;
}
#select-button {
  margin: auto auto;
}
.meal-selected {
  background-color: rgb(105, 180, 223) !important;
}

@keyframes button-animation {
  0% {
    left: 0.6px;
  }
  10% {
    left: 1.2px;
  }
  20% {
    left: 1.8px;
  }
  30% {
    left: 2.4px;
  }
  40% {
    left: 3px;
  }
  50% {
    left: 3.6px;
  }
  60% {
    left: 4.2px;
  }
  70% {
    left: 4.8px;
  }
  80% {
    left: 5.4px;
  }
  90% {
    left: 6px;
  }
  100% {
    left: 6.6px;
  }
}
</style>
