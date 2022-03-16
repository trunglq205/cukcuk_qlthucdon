<template>
  <div class="m-dialog">
    <div class="dialog-content">
      <div class="form-detail">
        <div class="form-detail-header">
          <div class="header-title">Thêm món</div>
          <div>
            <base-icon icon="close" @click="closeFormDetail()" />
          </div>
        </div>

        <div class="tab-ft">
          <div class="tab-form">
            <v-tabs v-model="tabs" height="28px" background-color="purple">
              <v-tab>Thông tin chung</v-tab>
              <v-tab>Sở thích phục vụ</v-tab>
            </v-tabs>
            <v-tabs-items v-model="tabs">
              <v-tab-item>
                <div class="form-info">
                  <div class="info-left">
                    <div class="label-form">
                      <div>
                        <label for=""
                          >Tên món
                          <span class="field-required">(*)</span></label
                        >
                      </div>
                      <div>
                        <label for=""
                          >Mã món <span class="field-required">(*)</span></label
                        >
                      </div>
                      <div>
                        <label for="">Nhóm thực đơn</label>
                      </div>
                      <div>
                        <label for=""
                          >Đơn vị tính
                          <span class="field-required">(*)</span></label
                        >
                      </div>
                      <div>
                        <label for=""
                          >Giá bán
                          <span class="field-required">(*)</span></label
                        >
                      </div>
                      <div>
                        <label for="">Giá vốn</label>
                      </div>
                      <div>
                        <label for="">Mô tả</label>
                      </div>
                      <div style="padding-top: 28px">
                        <label for="">Chế biến tại</label>
                      </div>
                    </div>

                    <div class="input-form">
                      <div class="form-ip ip1">
                        <base-input
                          v-model="menuName"
                          type="text"
                          rule="required"
                          autoFocus
                          :customErr="validateErrors.menuName"
                          idField="txtMenuName"
                          @touch="changeMenuCode()"
                        />
                      </div>
                      <div class="form-ip ip2">
                        <base-input
                          v-model="menuCode"
                          type="text"
                          rule="required"
                          :customErr="validateErrors.menuCode"
                          idField="txtMenuCode"
                        />
                      </div>
                      <div class="form-ip ip3">
                        <multiselect
                          v-model="valueGroupMenu"
                          :options="optionGroupMenu"
                          placeholder=""
                          label="name"
                          track-by="name"
                          :showNoResults="false"
                          :showNoOptions="false"
                        ></multiselect>
                      </div>
                      <div class="form-ip ip4">
                        <div class="ip-unit">
                          <multiselect
                            v-model="valueUnit"
                            :options="optionUnit"
                            placeholder=""
                            label="name"
                            track-by="name"
                            :showNoResults="false"
                            :showNoOptions="false"
                            :class="{ invalid: validateErrors.unit }"
                            id="selectUnit"
                          ></multiselect>
                        </div>
                        <div
                          class="icon-err"
                          :class="{ 'icon-invalid': validateErrors.unit }"
                        >
                          <base-icon :size="16" icon="exclamation" />
                          <div class="msg-err">
                            <base-icon :size="16" icon="exclamation" />
                            <p>Trường này không được để trống.</p>
                          </div>
                        </div>
                      </div>
                      <div class="form-ip ip5">
                        <base-input
                          type="number"
                          v-model="price"
                          rule="required"
                          :customErr="validateErrors.price"
                          idField="txtPrice"
                        />
                      </div>
                      <div class="form-ip ip6">
                        <base-input type="number" v-model="menu.investment" />
                      </div>
                      <div class="form-ip ip7">
                        <textarea
                          class="txtarea"
                          rows="3"
                          v-model="menu.description"
                        ></textarea>
                      </div>
                      <div class="form-ip ip8">
                        <multiselect
                          v-model="valueCookingAdress"
                          :options="optionCookingAdress"
                          placeholder=""
                          label="name"
                          track-by="name"
                          :showNoResults="false"
                          :showNoOptions="false"
                        ></multiselect>
                      </div>
                      <div class="form-ip ip9">
                        <div>
                          <base-checkbox v-model="menu.showInMenus"
                            >&nbsp;Không hiển thị trên thực đơn</base-checkbox
                          >
                        </div>
                      </div>
                    </div>

                    <div class="img-form">
                      <fieldset class="fieldset-img">
                        <legend>&nbsp;Ảnh đại diện&nbsp;</legend>
                        <div style="display: flex">
                          <div>
                            <img
                              src="../../assets/imgs/ImageHandler.png"
                              alt=""
                            />
                            <div class="iconSelectImg">
                              <base-button iconName="selectIconMenu" size="nor"
                                >Biểu tượng</base-button
                              >
                            </div>
                            <div class="text-avt">
                              Chọn các ảnh có định dạng
                              <div>(.jpg, .jpeg, .png, .gif)</div>
                            </div>
                          </div>
                          <div class="btn-avt">
                            <button>...</button>
                            <button>
                              <v-icon size="14">mdi-close-thick</v-icon>
                            </button>
                          </div>
                        </div>
                      </fieldset>
                    </div>
                  </div>
                  <div class="info-right"></div>
                </div>
              </v-tab-item>
              <v-tab-item>
                <div class="form-hobby">
                  <p>Món ăn:</p>
                  <div class="description">
                    <div class="icon-info">
                      <base-icon icon="info" :size="32" />
                    </div>
                    <div>
                      <p>
                        Ghi lại các sở thích của khách hàng giúp nhân viên phục
                        vụ chọn nhanh order.
                      </p>
                      <p>VD: Không cay/ít hành/thêm phomai...</p>
                    </div>
                  </div>
                  <div class="form-table-hobby">
                    <div class="tb-hobby">
                      <table class="m-table">
                        <thead>
                          <th>Sở thích phục vụ</th>
                          <th>Thu thêm</th>
                        </thead>
                        <tbody>
                          <tr
                            v-for="(item, index) in menu.serviceHobbies"
                            :key="index"
                            @click="getIndex(index)"
                          >
                            <td>
                              <multiselect
                                v-model="item.hobby"
                                :options="hobbyOptions"
                                placeholder=""
                                label="name"
                                track-by="name"
                                :showNoResults="false"
                                :showNoOptions="false"
                              ></multiselect>
                            </td>
                            <td>
                              <base-input
                                type="number"
                                :value="item.hobby ? item.hobby.price : 0"
                              />
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                    <div class="btn-table-hobby">
                      <base-button
                        iconName="fileNew"
                        size="nor"
                        @click="pushHobby()"
                        >Thêm dòng</base-button
                      >
                      <base-button
                        iconName="delete"
                        size="nor"
                        @click="removeHobby()"
                        >Xóa dòng</base-button
                      >
                    </div>
                  </div>
                </div>
              </v-tab-item>
            </v-tabs-items>
          </div>

          <div class="form-footer">
            <div class="footer-left">
              <base-button iconName="help" size="nor">Giúp</base-button>
            </div>
            <div class="footer-right">
              <base-button iconName="save" size="nor" @click="btnSave(1)"
                >Cất</base-button
              >
              <base-button iconName="saveAdd" size="nor"  @click="btnSave(2)"
                >Cất &amp; Thêm</base-button
              >
              <base-button
                iconName="disable"
                size="nor"
                @click="closeFormDetail()"
                >Hủy bỏ</base-button
              >
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import BaseIcon from "../../components/base/BaseIcon.vue";
import BaseInput from "../../components/base/BaseInput.vue";
import Multiselect from "vue-multiselect";
import BaseCheckbox from "../../components/base/BaseCheckbox.vue";
import BaseButton from "../../components/base/BaseButton.vue";
import axios from "axios";
export default {
  components: { BaseIcon, BaseInput, Multiselect, BaseCheckbox, BaseButton },
  props: ["menuID", "editMode"],
  data: () => ({
    menuCode: null,
    menuName: null,
    price: null,
    menu: {
      menuCode: null,
      menuName: null,
      menuGroup: null,
      unit: null,
      price: 0,
      investment: 0,
      description: null,
      cookingAddress: null,
      showInMenus: false,
      serviceHobbies: [],
    },
    hobbyOptions: [],
    tabs: null,
    optionGroupMenu: [{ name: "Lẩu" }, { name: "Nướng" }],
    valueGroupMenu: { name: "" },
    optionUnit: [{ name: "Chiếc" }, { name: "Cái" }],
    valueUnit: { name: "" },
    optionCookingAdress: [{ name: "Bếp" }],
    valueCookingAdress: { name: "" },
    closeForm: true,
    num: 1,
    checkLoad: false,
    rowIndex: null,
    validateErrors: {
      menuName: false,
      menuCode: false,
      unit: false,
      price: false,
    },
  }),
  created() {
    this.getHobby();
    if (this.menuID) {
      this.getMenuByID(this.menuID);
      this.getServiceHobby(this.menuID);
    }
  },
  watch: {
    menuName() {
      if (!this.menuName) {
        this.validateErrors.menuName = true;
      } else {
        this.validateErrors.menuName = false;
      }
    },
    menuCode() {
      if (!this.menuCode) {
        this.validateErrors.menuCode = true;
      } else {
        this.validateErrors.menuCode = false;
      }
    },
    price() {
      if (!this.price) {
        this.validateErrors.price = true;
      } else {
        this.validateErrors.price = false;
      }
    },
    valueUnit() {
      if (!this.valueUnit) {
        this.validateErrors.unit = true;
      } else {
        this.validateErrors.unit = false;
      }
    },
  },
  methods: {
    /**
     - Lấy dữ liệu thực đơn theo ID
     - Author: LQTrung(28/2/2022)
     */
    getMenuByID(menuID) {
      var me = this;
      axios
        .get(`/menus/${menuID}`)
        .then(function (res) {
          console.log(res);
          me.menu = res.data;
          me.menuName = me.menu.menuName;
          me.menuCode = me.menu.menuCode;
          me.valueGroupMenu.name = me.menu.menuGroup;
          me.valueUnit.name = me.menu.unit;
          me.price = me.menu.price;
          me.valueCookingAdress.name = me.menu.cookingAddress;
          if (me.editMode == 0) {
            me.menu.menuCode = "";
          }
        })
        .catch(function (err) {
          console.log(err);
        });
    },
    /**
     - Lấy dữ liệu sở thích phục vụ theo thực đơn ID
     - Author: LQTrung(28/2/2022)
     */
    getServiceHobby(menuID) {
      var me = this;
      axios
        .get(`servicehobbys/${menuID}`)
        .then(function (res) {
          me.menu.serviceHobbies = res.data;
          me.dataSubmit(2);
        })
        .catch(function (err) {
          console.log(err);
        });
    },

    /**
     - Lấy dữ liệu sở thích phục vụ
     - Author: LQTrung(28/2/2022)
     */
    getHobby() {
      var me = this;
      axios
        .get("hobbys")
        .then(function (res) {
          console.log(res);
          me.hobbyOptions = res.data.map((e) => {
            return {
              id: e.hobbyID,
              name: e.hobbyName,
              price: e.extraPrice,
            };
          });
          console.log(me.hobbyOptions);
        })
        .catch(function (err) {
          console.log(err);
        });
    },

    /**
     - Kiểm tra hợp lệ dữ liệu
     - Author: LQTrung(28/2/2022)
     */
    validateData() {
      var isValid = true;
      this.validateErrors = {
        menuName: false,
        menuCode: false,
        unit: false,
        price: false,
      };
      if (!this.menuName) {
        this.validateErrors.menuName = true;
        isValid = false;
      }
      if (!this.menuCode) {
        this.validateErrors.menuCode = true;
        isValid = false;
      }
      if (!this.valueUnit.name) {
        this.validateErrors.unit = true;
        isValid = false;
      }
      if (!this.price) {
        this.validateErrors.price = true;
        isValid = false;
      }
      return isValid;
    },

    /**
     - Thêm mới thực đơn
     - Author: LQTrung(28/2/2022)
     */
    insertMenu(type) {
      var me = this;
      this.menu = this.dataSubmit(1);
      console.log(this.menu);
      axios
        .post("menus", this.menu)
        .then(function (res) {
          console.log(res);
          if(type==1){
            me.closeFormDetail();
          }
          me.checkLoad = true;
          me.$emit("loadData", me.checkLoad);
        })
        .catch(function (err) {
          console.log(err);
        });
    },
    /**
     - Cập nhật thông tin thực đơn
     - Author: LQTrung(28/2/2022)
     */
    updateMenu(type) {
      var me = this;
      this.menu = this.dataSubmit(1);
      axios
        .put(`menus/${this.menuID}`, this.menu)
        .then(function (res) {
          console.log(res);
          me.checkLoad = true;
          if(type==1){
            me.closeFormDetail();
          }
          me.checkLoad = true;
          me.$emit("loadData", me.checkLoad);
        })
        .catch(function (err) {
          console.log(err);
        });
    },
    /**
     - Button cất dữ liệu
     - Author: LQTrung(28/2/2022)
     */
    btnSave(type) {
      if (this.validateData()) {
        switch (this.editMode) {
          case 0:
            this.insertMenu(type);
            break;
          case 1:
            this.updateMenu(type);
            break;
        }
      } else {
        if (!this.menuName) {
          document.getElementById("txtMenuName").focus();
        } else if (!this.menuCode) {
          document.getElementById("txtMenuCode").focus();
        } else if (!this.valueUnit) {
          document.getElementById("selectUnit").focus();
        } else if (!this.price) {
          document.getElementById("txtPrice").focus();
        }
      }
    },
    /**
     - Đóng form chi tiết thực đơn
     - Author: LQTrung(28/2/2022)
     */
    closeFormDetail() {
      this.$emit("closeForm", this.closeForm);
    },
    //định dạng tiền tệ
    formatCash(str) {
      return str
        .split("")
        .reverse()
        .reduce((prev, next, index) => {
          return (index % 3 ? next : next + ".") + prev;
        });
    },
    
    /**
     - Định dạng thông tin menu
     - Author: LQTrung(28/2/2022)
     */
    dataSubmit(request) {
      var service = this.menu.serviceHobbies;
      switch (request) {
        // gửi lên server
        case 1:
          this.menu.menuName = this.menuName;
          this.menu.menuCode = this.menuCode;
          this.menu.menuGroup = this.valueGroupMenu.name;
          this.menu.unit = this.valueUnit.name;
          this.menu.price = this.price;
          this.menu.cookingAddress = this.valueCookingAdress.name;
          service = service
            .map((e) => {
              if (e.hobby) {
                return {
                  hobby: {
                    hobbyID: e.hobby.id,
                    hobbyName: e.hobby.name,
                    extraPrice: e.hobby.price,
                  },
                };
              }
            })
            .filter((x) => x != null);
          return { ...this.menu, serviceHobbies: service };
        case 2:
          // lấy từ server
          service = service.map((e) => {
            return {
              hobby: {
                id: e.HobbyID,
                name: e.HobbyName,
                price: e.ExtraPrice,
              },
            };
          });
          this.menu.serviceHobbies = service;
          break;
      }
    },
    /**
     - Thêm một object vào array menu.serviceHobbies
     - Author: LQTrung(28/2/2022)
     */
    pushHobby() {
      this.menu.serviceHobbies.push({ hobby: null });
    },
    /**
     - Lấy vị trí dòng trong table tab sở thích phục vụ
     - Author: LQTrung(28/2/2022)
     */
    getIndex(value) {
      this.rowIndex = value;
    },
    /**
     - Xóa dòng trong table tab sở thích phục vụ
     - Author: LQTrung(28/2/2022)
     */
    removeHobby() {
      this.menu.serviceHobbies.splice(this.rowIndex, 1);
    },
    /**
     - Cập nhật MenuCode theo MenuName
     - Author: LQTrung(28/2/2022)
     */
    changeMenuCode() {
      if (this.menuName) {
        var me = this;
        axios
          .get(`menus/get-menucode/${this.menuName}`)
          .then(function (res) {
            console.log(res);
            console.log(me.menuName);
            me.menuCode = res.data;
          })
          .catch(function (err) {
            console.log(err);
          });
      }
    },
  },
};
</script>

<style scoped src="../../assets/css/view/menuDetail.css"></style>
