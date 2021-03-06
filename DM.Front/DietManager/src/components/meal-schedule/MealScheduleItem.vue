<template>
  <div id="schedule-item" class="content-background">
    <modal v-if="showDeleteModal" class="modal-window">
      <div id="delete-modal-content">
        <h4>Are you sure you want to delete this item?</h4>
        <div class="modal-buttons-container">
          <button class="button main-background-color" @click="scheduleChanged('Delete')">Delete</button>
          <button class="button main-background-color" @click="showDeleteModal = false">Cancel</button>
        </div>
      </div>
    </modal>

    <modal v-if="showEditModal">
      <div id="edit-modal-content">
        <h4>Set a new date</h4>
        <div class="time-picker">
          <input type="time" class="form-control" v-model="time">
        </div>
        <div class="modal-buttons-container">
          <button class="button main-background-color" @click="scheduleChanged('Update')">Save</button>
          <button class="button main-background-color" @click="showEditModal = false">Cancel</button>
        </div>
      </div>
    </modal>

    <div id="date">{{date}}</div>

    <div id="image-and-buttons">
      <button
        id="edit-button"
        class="button main-background-color hoverable"
        @click="showEditModal = true"
      >
        <font-awesome-icon icon="pencil-alt"/>
      </button>
      <div id="image-container">
        <image-wrapper :imageId="mealScheduleEntry.meal.imageId" :asWheel="true">
          <template slot="placeholder">
            <font-awesome-icon class="main-color" icon="utensils"/>
          </template>
        </image-wrapper>
      </div>
      <button
        id="delete-button"
        class="button main-background-color hoverable"
        @click="showDeleteModal = true"
      >
        <font-awesome-icon icon="trash-alt"/>
      </button>
    </div>
    <div id="meal-name">{{mealScheduleEntry.meal.name}}</div>
    <button
      id="details-button"
      class="button main-background-color hoverable"
      @click="showDetails"
    >Details</button>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import MealScheduleEntry from "@/ViewModels/meal-schedule/mealScheduleEntry";
import { Actions } from "@/ViewModels/enums/actions";
import { Prop, Emit } from "vue-property-decorator";
import Modal from "@/components/common/Modal.vue";
import MealScheduleApiCaller from "@/services/api-callers/mealScheduleApi";
import MealScheduleEntryUpdate from "@/ViewModels/meal-schedule/mealScheduleEntryUpdate";
import ImageWrapper from "@/components/image/ImageWrapper.vue";

@Component({
  components: {
    modal: Modal,
    "image-wrapper": ImageWrapper
  }
})
export default class MealScheduleItem extends Vue {
  @Prop({
    required: true
  })
  mealScheduleEntry!: MealScheduleEntry;
  showDeleteModal: boolean = false;
  showEditModal: boolean = false;

  time!: string;

  mounted() {
    this.time = this.getEntryDateTimeString();
  }

  get date() {
    let dateString: string = "";

    const date = new Date(this.mealScheduleEntry.date);

    const hours = date.getHours();
    const minutes = date.getMinutes();

    if (hours < 10) {
      dateString = "0" + hours + ":";
    } else {
      dateString = hours + ":";
    }

    if (minutes < 10) {
      dateString += "0" + minutes;
    } else {
      dateString += minutes;
    }

    return dateString;
  }

  getEntryDateTimeString() {
    const date = new Date(this.mealScheduleEntry.date);
    return `${date.getHours()}:${date.getMinutes()}`;
  }

  get dateWithUpdatedTime() {
    if (this.time === "" || this.time.length != 5) {
      return this.mealScheduleEntry.date;
    }

    const hourAndMinutes = this.time.split(":");

    const newDate = new Date(this.mealScheduleEntry.date);
    newDate.setHours(parseInt(hourAndMinutes[0]), parseInt(hourAndMinutes[1]));

    return newDate;
  }

  @Emit()
  scheduleChanged(actions: string) {
    if (actions === Actions[Actions.Delete]) {
      this.showDeleteModal = false;
      return { action: Actions[Actions.Delete], id: this.mealScheduleEntry.id };
    } else {
      this.showEditModal = false;
      return {
        action: Actions[Actions.Update],
        id: this.mealScheduleEntry.id,
        newDate: this.dateWithUpdatedTime
      };
    }
  }

  showDetails() {
    this.$router.push({ path: "/meal/" + this.mealScheduleEntry.meal.id });
  }
}
</script>

<style lang="less" scoped>
#date {
  background-color: white;
  border-radius: 10px;
  margin-bottom: 10px;
}
#schedule-item {
  margin: 5px;
  padding: 5px;
  border-radius: 10px;
}
.hoverable {
  visibility: hidden;
}

#schedule-item:hover {
  .hoverable {
    visibility: initial;
  }
}
button {
  color: white;
}
#image-container {
  border-radius: 50%;
  width: 45px;
  height: 45px;
  align-content: center;
  > * {
    width: 100%;
    height: 100%;
  }
}

#meal-name {
  padding: 5px;
  overflow-wrap: break-word;
}

#image-and-buttons {
  align-content: center;
  width: 98%;
  display: inline-flex;
  justify-content: space-evenly;
  button {
    width: 28px;
    height: 28px;
    text-align: center;
    padding: 5px;
    > * {
      position: relative;
      top: -3px;
    }
  }
}
#details-button {
  padding-top: 5px;
}
#edit-modal-content {
  > * {
    margin: 10px;
  }
}
.time-picker {
  display: inline-flex;
}
input {
  width: 150px;
}
.modal-buttons-container {
  button {
    height: 40px;
  }
}
</style>
