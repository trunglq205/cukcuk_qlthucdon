<template>
  <div class="m-dialog">
    <div class="dialog-content">
      <div class="dialog-title">
        <span>CUKCUK - Quản lý nhà hàng</span>
      </div>
      <div class="dialog-inner">
        <div class="dialog-body">
          <base-icon class="dialog-icon" :size="32" :icon="icon" />
          <span class="dialog-text">{{ text }}</span>
        </div>
        <div
          :class="[
            'dialog-footer',
            confirm && 'dialog-footer-confirm',
            ok && 'dialog-footer-ok',
            change && 'dialog-footer-change',
          ]"
        >
          <base-button
            v-if="change"
            color="secondary"
            size="nor"
            @click="$emit('cancel')"
            >Hủy</base-button
          >
          
          <base-button
            v-if="confirm"
            color="primary"
            size="nor"
            @click="$emit('yes')"
            >Có</base-button
          >

          <base-button
            class="btn-no"
            v-if="confirm"
            color="secondary"
            size="nor"
            @click="$emit('no')"
            >Không</base-button
          >

          <base-button
            v-if="notic"
            color="primary"
            size="nor"
            @click="$emit('close')"
            >Đóng</base-button
          >
          <base-button
            v-if="ok"
            color="primary"
            size="nor"
            @click="$emit('close')"
            >Đồng ý</base-button
          >
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import BaseButton from "./BaseButton.vue";
import BaseIcon from "./BaseIcon.vue";
export default {
  components: { BaseButton, BaseIcon },
  props: {
    /**
     * tên icon
     */
    icon: String,
    /**
     * message
     */
    text: String,
    /**
     * dạng yes - no
     */
    confirm: Boolean,
    /**
     * dạng thông báo button Đóng
     */
    notic: Boolean,
    /**
     * dạng thông báo button Đồng ý
     */
    ok: Boolean,
    /**
     * thông báo thay đổi
     */
    change: Boolean,
  },
  emits: ["yes", "no", "close"],
};
</script>
<style scoped src="../../assets/css/base/dialog.css"></style>
