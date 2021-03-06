<template>
  <div id="account-manager">
    <modal v-if="showDeleteAccountModal">
      <span class="modal-text">Are you sure you want to delete your account?
        <br>All the created meals and meal ingredients will remain.
      </span>
      <div class="modal-buttons-container">
        <button class="button" @click="deleteAccount">Delete</button>
        <button class="button" @click="showDeleteAccountModal = false">Cancel</button>
      </div>
    </modal>
    <div id="image">
      <div id="user-image">
        <image-uploader
          :predefinedImageId="user.imageId"
          @addImage="upsertAvatar"
          @deleteImage="deleteAvatar"
        >
          <template slot="placeholder">
            <font-awesome-icon
              id="placeholder-icon"
              class="user-avatar main-color"
              icon="user-circle"
            />
          </template>
        </image-uploader>
      </div>
    </div>
    <div id="user-name">{{user.name + ' ' + user.surname}}</div>
    <div id="user-city">{{user.city}}</div>
    <div id="delete-account" class="soft-border top">
      <button class="button" @click="showDeleteAccountModal = true">Delete your account</button>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import ImageWrapper from "@/components/image/ImageWrapper.vue";
import UserApiCaller from "@/services/api-callers/userApi";
import AuthService from "@/services/authService";
import User from "@/ViewModels/user/user";
import UserUpdate from "@/ViewModels/user/userUpdate";
import ImageApiCaller from "@/services/api-callers/imageApi";
import ImageUploader from "@/components/image/ImageUploader.vue";
import Modal from "@/components/common/Modal.vue";
import { EventBus } from "@/services/eventBus";

Component.registerHooks(["beforeRouteEnter"]);

@Component({
  components: {
    "image-wrapper": ImageWrapper,
    "image-uploader": ImageUploader,
    modal: Modal
  }
})
export default class AccountManager extends Vue {
  private user: User = {
    id: "",
    name: "",
    surname: "",
    imageId: null,
    city: "",
    isFriend: null
  };
  private userUpdate: UserUpdate = this.unsettedUserUpdateValue;
  private errors: string[] = [];
  private showDeleteAccountModal = false;

  beforeRouteEnter(
    to: any,
    from: any,
    next: (onBeforeRouteEnter: (instance: AccountManager) => void) => void
  ) {
    next(instance => {
      instance.reloadLoggedInUser();
    });
  }

  created() {
    this.assignNewUser(this.emptyUser);
  }

  get emptyUser() {
    return {
      id: "",
      name: "",
      surname: "",
      imageId: null,
      city: "",
      isFriend: null
    } as User;
  }

  get unsettedUserUpdateValue() {
    return {
      name: this.user.name,
      surname: this.user.name,
      city: this.user.city,
      imageId: this.user.imageId,
      password: ""
    } as UserUpdate;
  }

  getLoggedInUser() {
    const loggedInUser = AuthService.getloggedInUser();
    if (loggedInUser) {
      this.assignNewUser(loggedInUser);
    } else {
      this.assignNewUser(this.emptyUser);
    }
    this.userUpdate = this.unsettedUserUpdateValue;
  }

  assignNewUser(user: User) {
    this.user = Object.assign({}, this.user, user);
  }

  deleteAccount() {
    AuthService.deleteAccount(
      () => this.$router.push({ name: "SignIn" }),
      this.onDeleteAccountError
    );
  }

  onDeleteAccountError(errors: string[]) {}

  deleteAvatar() {
    UserApiCaller.deleteUserAvatar(() => {
      this.user.imageId = null;
      EventBus.$emit("reload-user-data");
    }, this.onDeleteAvatarError);
  }

  upsertAvatar(id: string) {
    UserApiCaller.upsertUserAvatar(
      id,
      id => {
        this.$set(this.user, "imageId", id);
        EventBus.$emit("reload-user-data");
      },
      this.onDeleteAvatarError
    );
  }

  onDeleteAvatarError(errors: string[] | null) {
    if (errors) {
      this.errors.push(...errors);
    }
  }

  reloadLoggedInUser() {
    AuthService.reloadLoggedInUser(user => {
      this.assignNewUser(user);
    }, this.onReloadLoggedInUserError);
  }

  onReloadLoggedInUserError() {}
}
</script>

<style lang="less" scoped>
#account-manager {
  width: 100%;
}
#placeholder-icon {
  width: 250px;
  height: 250px;
  > * {
    width: 100%;
    height: 100%;
  }
}
#image {
  display: inline-flex;
}
#delete-account {
  text-align: right;
  margin-top: 15px;
  min-width: 100%;
  height: 100px;
  > * {
    margin-top: 10px;
    height: 30px;
    font-size: 0.8em;
    letter-spacing: 1.5px;
  }
}
</style>
