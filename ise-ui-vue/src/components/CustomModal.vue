<template>
  <transition name="modal-animation">
    <div :id="'customModal-' + modalId" style="display:none"  class="modal" :class="'customModal-' + modalId">
      <transition name="modal-animation-inner">
        <div class="modal-inner" v-show=modalActive :class="'customModal-' + modalId">
          <div style="float:left">
            <h3 class="card-header">{{modalTitle}}</h3>
          </div>
          <div style="float:right">
            <button type="button" class="close" 
              @click="close"> X 
          </button>
          </div>
          <!--<i @click="close" class="">minimize</i>-->
          <!-- Modal Content -->
          <!--<label>TTT{{modalId}}</label>-->
          <slot />
          <button class="btn btn-outline-success btn-light me-2 modalCloseBt" @click="close" type="button">Cancel</button>
        </div>
      </transition>
    </div>
  </transition>
</template>
<script>
export default {
  props: ["modalId", "modalActive", "modalTitle"],
  setup(props, { emit }) {
    const close = () => {
      emit("close");
    };
    return { close };
  },
};
</script>
<style>
.modal-animation-enter-active,
.modal-animation-leave-active {
  transition: opacity 0.3s cubic-bezier(0.52, 0.02, 0.19, 1.02);
}
.modal-animation-enter-from,
.modal-animation-leave-to {
  opacity: 0;
}
.modal-animation-inner-enter-active {
  transition: all 0.3s cubic-bezier(0.52, 0.02, 0.19, 1.02) 0.15s;
}
.modal-animation-inner-leave-active {
  transition: all 0.3s cubic-bezier(0.52, 0.02, 0.19, 1.02);
}
.modal-animation-inner-enter-from {
  opacity: 0;
  transform: scale(0.8);
}
.modal-animation-inner-leave-to {
  transform: scale(0.8);
}
.modal {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  width: 100vw;
  position: fixed;
  top: 0;
  left: 0;
  background-color: rgb(0 0 0 / 20%);
}

.modal-inner {
    position: relative;
    max-width: 840px;
    margin: auto;
    width: 90%;
    box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06);
    background-color: #fff;
    padding: 10px 10px;
    overflow-y:auto;
    max-height:95%;
  }

  .modal-inner i {
      position: absolute;
      top: 15px;
      right: 15px;
      font-size: 20px;
      cursor: pointer;
    }
    .modal-inner .modalCloseBt {
      margin-top: 10px;
      float: right;
    }
</style>