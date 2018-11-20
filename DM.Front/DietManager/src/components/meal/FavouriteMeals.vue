// todo: reuse MyMealsComponent
<template>
  <div class="meals-container">
    <h1 class="main-color">Favourite Meals</h1>
    <meal-preview-item class="meal" v-for="mealPreview in mealPreviews" :key="mealPreview.id" :mealPreview="mealPreview" :enableFavouriteMarkToggling="true" @deleted-from-favourites="onDeletedFromFavourites" />
    <button @click="getMealPreviews" class="load-more-button main-background-color" v-if="elementsRemainingToLoad">
      Load more...
    </button>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";

import MealPreview from "@/ViewModels/meal/mealPreview";
import MealPreviewWithImage from "@/ViewModels/meal/mealPreviewWithImage";
import FavouritesApiCaller from "@/services/api-callers/favouritesApi";
import IndexedResult from "@/ViewModels/wrappers/indexedResult";
import ImageApiCaller from "@/services/api-callers/imageApi";
import MealPreviewItem from "./MealPreviewItem.vue";

Component.registerHooks(["beforeRouteEnter"]);

@Component({
  components: {
    "meal-preview-item": MealPreviewItem
  }
})
export default class MyMeals extends Vue {
  private mealPreviews: MealPreviewWithImage[] = [];
  private lastReturned: IndexedResult<MealPreview> | null = null;

  beforeRouteEnter(
    to: any,
    from: any,
    next: (onBeforeRouteEnter: (instance: MyMeals) => void) => void
  ) {
    next(instance => {
      instance.getMealPreviews();
    });
  }

  get isMobile() {
    if (window.innerWidth < 860) {
      return true;
    } else {
      return false;
    }
  }

  get elementsRemainingToLoad() {
    if (!this.lastReturned) {
      return true;
    } else {
      return !this.lastReturned.isLast;
    }
  }

  getMealPreviews() {
    FavouritesApiCaller.get(
      this.lastReturned,
      this.getMealPreviewsSuccessHandler
    );
  }

  getMealPreviewsSuccessHandler(
    indexedMealPreviews: IndexedResult<MealPreview[]>
  ) {
    if (
      !indexedMealPreviews.result !== null ||
      indexedMealPreviews.result.length > 0
    ) {
      this.mealPreviews.push(
        ...indexedMealPreviews.result.map(mealPreview => {
          const mealPreviewWithImage = {
            mealPreview: mealPreview,
            imageData: null
          } as MealPreviewWithImage;
          this.getMealPreviewImage(mealPreviewWithImage, mealPreview.imageId);
          return mealPreviewWithImage;
        })
      );

      this.lastReturned = {
        result:
          indexedMealPreviews.result[indexedMealPreviews.result.length - 1],
        index: indexedMealPreviews.index,
        isLast: indexedMealPreviews.isLast
      };
    }
  }

  getMealPreviewImage(
    mealPreviewWithImage: MealPreviewWithImage,
    imageGuid: string | null
  ) {
    if (imageGuid !== null) {
      ImageApiCaller.get(imageGuid, imageData => {
        mealPreviewWithImage.imageData = imageData;
      });
    }
  }

  onDeletedFromFavourites(guid: string) {
    const indexOfDeletedItem = this.mealPreviews.findIndex(
      m => m.mealPreview.id == guid
    );

    this.mealPreviews.splice(indexOfDeletedItem, 1);
  }
}
</script>

<style lang="less">
h1 {
  margin-bottom: 15px !important;
}
.meals-container {
  padding: 10px;
  display: block;
  background-color: #e6e4e4;
  border-radius: 10px;
  min-height: 800px;
}
.meal {
  text-align: left;
  background-color: white;
  margin-bottom: 10px;
  border-radius: 10px;
  padding: 5px;
  display: flex;
  justify-content: space-between;
  text-align: center;
}
.load-more-button {
  border-radius: 7px;
  padding: 5px 10px 28px 10px;
  height: 30px;
  text-align: center;
  color: white;
}
</style>