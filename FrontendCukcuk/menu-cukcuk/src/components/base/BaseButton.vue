<template>
    <button 
    :class="`m-button m-btn-${color} m-btn-${size} m-btn-${iconName}`"
    @click="$emit('click')">

    <base-icon v-if="iconName" class="btn-icon" :icon="iconName" />
    <slot/>

    </button>
</template>
<script>
import BaseIcon from './BaseIcon.vue'
export default {
  components: { BaseIcon },
  emits: ["click"],
  props: {
    /**
     * màu button
     * @values  primary, secondary, high
     */
    color: {
      type: String,
      default: "primary",
    },
    /**
     * kích thước button
     * @values sm, md, nor
     */
    size: {
      type: String,
      default: "sm",
    },
    iconName: {
        type: String,
    }
  },
}
</script>
<style scoped src="../../assets/css/base/button.css"></style>