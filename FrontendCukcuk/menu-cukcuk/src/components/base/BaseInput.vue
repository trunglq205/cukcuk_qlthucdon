<template>
  <!-- <ValidationObserver>
    <ValidationProvider ref="provider" :rules="rule" v-slot="{ errors }"> -->
      <div :class="['m-input', `m-input-${type}`]">
        <input
          :type="type"
          v-bind="$attrs"
          :value="value"
          @input="onInput"
          @blur="$emit('touch')"
          ref="input"
          :class="{'invalid': customErr}"
          :title="titleErr"
          :id="idField"
          @focus="onFocus"
        />
        <div class="icon-err" :class="{'icon-invalid': customErr}">
          <base-icon :size="16" icon="exclamation" />
          <div class="msg-err">
            <base-icon :size="16" icon="exclamation"/>
            <p>Trường này không được để trống.</p>
          </div>
        </div>
      </div>
    <!-- </ValidationProvider>
  </ValidationObserver> -->
</template>
<script>
import BaseIcon from './BaseIcon.vue';
export default {
  components: { BaseIcon },
  props: {
    /**
     * loại input
     */
    type: String,
    /**
     * validation
     */
    rule: String,
    /**
     * @model
     */
    value: [String, Number],
    /**
     * tự động focus
     */
    autoFocus: Boolean,
    /**
     * clear lỗi
     */
    isRemoveErr: {
      type: Boolean,
      default: false,
    },
    /**
     * title lỗi khi hover
     */
    titleErr: String,
    /**
     * id của input
     */
    idField: String,
    customErr: {
      type: Boolean,
      default: false
    }
  },
  watch: {
    //theo dõi sự thay đổi của isRemove để làm mới provider
    isRemoveErr() {
      if (this.isRemoveErr) {
        this.$refs.provider.reset();
      }
    },
  },
  methods: {
    onInput(event) {
      this.$emit("input", event.target.value);
    },
    onFocus() {
      this.$refs.input.select();
    },
  },
  mounted() {
    if (this.autoFocus) {
      this.$refs.input.focus();
    }
  },
};
</script>
<style scoped src="../../assets/css/base/input.css"></style>
