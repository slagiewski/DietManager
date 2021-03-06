<template>
  <ul class="content-background">
    <li>
      <div class="element-name">Calories</div>
      <div class="element-value">{{caloriesSummary}}</div>
    </li>
    <li>
      <div class="element-name">Proteins</div>
      <div class="element-value">{{proteinsSummary}}</div>
    </li>
    <li>
      <div class="element-name">Carbohydr.</div>
      <div class="element-value">{{carbohydratesSummary}}</div>
    </li>
    <li>
      <div class="element-name">Fats</div>
      <div class="element-value">{{fatsSummary}}</div>
    </li>
    <li>
      <div class="element-name">Vit. A</div>
      <div class="element-value">{{vitaminASummary}}</div>
    </li>
    <li>
      <div class="element-name">Vit. C</div>
      <div class="element-value">{{vitaminCSummary}}</div>
    </li>
    <li>
      <div class="element-name">Vit. B6</div>
      <div class="element-value">{{vitaminB6Summary}}</div>
    </li>
    <li>
      <div class="element-name">Vit. D</div>
      <div class="element-value">{{vitaminDSummary}}</div>
    </li>
  </ul>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { Prop } from "vue-property-decorator";
import _ from "lodash";

import MealIngredientWithQuantity from "@/ViewModels/meal-ingredient/mealIngredientWithQuantity";

@Component
export default class MealSummary extends Vue {
  @Prop({
    type: Array,
    required: true
  })
  private mealIngredients!: MealIngredientWithQuantity[];

  get caloriesSummary() {
    let result = _.sum(
      this.mealIngredients.map(
        ingredientWithQuantity =>
          (ingredientWithQuantity.mealIngredient.calories *
            ingredientWithQuantity.quantity) /
          100
      )
    );

    return this.finalizeResult(result, "kcal");
  }

  get proteinsSummary() {
    let result = _.sum(
      this.mealIngredients.map(
        ingredientWithQuantity =>
          (ingredientWithQuantity.mealIngredient.nutrition.protein *
            ingredientWithQuantity.quantity) /
          100
      )
    );

    return this.finalizeResult(result, "g");
  }

  get carbohydratesSummary() {
    let result = _.sum(
      this.mealIngredients.map(
        ingredientWithQuantity =>
          (ingredientWithQuantity.mealIngredient.nutrition.carbohydrates *
            ingredientWithQuantity.quantity) /
          100
      )
    );

    return this.finalizeResult(result, "g");
  }

  get fatsSummary() {
    let result = _.sum(
      this.mealIngredients.map(
        ingredientWithQuantity =>
          (ingredientWithQuantity.mealIngredient.nutrition.fats *
            ingredientWithQuantity.quantity) /
          100
      )
    );

    return this.finalizeResult(result, "g");
  }

  get vitaminASummary() {
    let result = _.sum(
      this.mealIngredients.map(ingredientWithQuantity => {
        // eslint-disable-next-line no-console
        console.log(
          "meal-ingredient quantity: " + ingredientWithQuantity.quantity
        );
        if (
          !this.isNullOrUndefined(
            ingredientWithQuantity.mealIngredient.nutrition.vitaminA
          )
        ) {
          return (
            (ingredientWithQuantity.mealIngredient.nutrition.vitaminA! *
              ingredientWithQuantity.quantity) /
            100
          );
        } else {
          return 0;
        }
      })
    );

    return this.finalizeResult(result, "mg");
  }

  get vitaminCSummary() {
    let result = _.sum(
      this.mealIngredients.map(ingredientWithQuantity => {
        if (
          !this.isNullOrUndefined(
            ingredientWithQuantity.mealIngredient.nutrition.vitaminC
          )
        ) {
          return (
            (ingredientWithQuantity.mealIngredient.nutrition.vitaminC! *
              ingredientWithQuantity.quantity) /
            100
          );
        } else {
          return 0;
        }
      })
    );

    return this.finalizeResult(result, "mg");
  }

  get vitaminB6Summary() {
    let result = _.sum(
      this.mealIngredients.map(ingredientWithQuantity => {
        if (
          !this.isNullOrUndefined(
            ingredientWithQuantity.mealIngredient.nutrition.vitaminB6
          )
        ) {
          return (
            (ingredientWithQuantity.mealIngredient.nutrition.vitaminB6! *
              ingredientWithQuantity.quantity) /
            100
          );
        } else {
          return 0;
        }
      })
    );

    return this.finalizeResult(result, "mg");
  }

  get vitaminDSummary() {
    let result = _.sum(
      this.mealIngredients.map(ingredientWithQuantity => {
        if (
          !this.isNullOrUndefined(
            ingredientWithQuantity.mealIngredient.nutrition.vitaminD
          )
        ) {
          return (
            (ingredientWithQuantity.mealIngredient.nutrition.vitaminD! *
              ingredientWithQuantity.quantity) /
            100
          );
        } else {
          return 0;
        }
      })
    );

    return this.finalizeResult(result, "mg");
  }

  private isNullOrUndefined(argument: any) {
    if (typeof argument === "undefined" || argument === null) {
      return true;
    } else {
      return false;
    }
  }

  private finalizeResult(result: number, suffix: string) {
    if (!result) {
      return `-- ${suffix}`;
    }

    return `${Math.round(result * 100) / 100} ${suffix}`;
  }
}
</script>

<style scoped>
ul {
  list-style: none;
  border-radius: 10px;
}
li {
  justify-content: space-between;
  display: flex;
  border-bottom-width: 1px;
  border-bottom-style: solid;
  border-color: rgb(190, 190, 190);
  margin: 15px 0px;
  font-size: 16px;
}
.element-name {
  text-align: left;
}
.element-value {
  text-align: right;
}
</style>