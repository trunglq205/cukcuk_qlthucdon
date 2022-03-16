<template>
  <div class="page-menus">
    <div class="page-title">
      <div class="m-title">Thực đơn</div>
      <div>
        <base-button size="md" iconName="viewEmail">Phản hồi</base-button>
      </div>
    </div>

    <div class="page-content">
      <div class="toolbar">
        <div class="toolbar-item">
          <base-button iconName="fileNew" @click="btnAdd()">Thêm</base-button>
        </div>
        <div class="toolbar-item">
          <base-button iconName="cloned" @click="btnClone()"
            >Nhân bản</base-button
          >
        </div>
        <div class="toolbar-item">
          <base-button iconName="edit" @click="btnEdit()">Sửa</base-button>
        </div>
        <div class="toolbar-item">
          <base-button iconName="delete" @click="btnDelete()">Xóa</base-button>
        </div>
        <div class="toolbar-item">
          <base-button iconName="refresh" @click="getMenus()">Nạp</base-button>
        </div>
      </div>

      <div class="table-menus">
        <table class="m-table" border="1">
          <thead>
            <th>
              <div class="th-text">Loại món</div>
              <div class="th-filter text-align-left" style="width: 150px">
                <multiselect
                  v-model="valueType"
                  :options="optionsType"
                  placeholder=""
                  label="name"
                  track-by="name"
                  :showNoResults="false"
                  :showNoOptions="false"
                ></multiselect>
              </div>
            </th>
            <th>
              <div class="th-text">Mã món</div>
              <div class="th-filter">
                <div>
                  <button class="btn-filter">
                    <span>∗</span>
                  </button>
                </div>
                <div style="width: 105px">
                  <base-input />
                </div>
              </div>
            </th>
            <th>
              <div class="th-text">Tên món</div>
              <div class="th-filter">
                <div>
                  <button class="btn-filter">
                    <span>∗</span>
                  </button>
                </div>
                <div style="width: 155px">
                  <base-input />
                </div>
              </div>
            </th>
            <th>
              <div class="th-text">Nhóm thực đơn</div>
              <div class="th-filter">
                <div>
                  <button class="btn-filter">
                    <span>∗</span>
                  </button>
                </div>
                <div style="width: 120px">
                  <base-input />
                </div>
              </div>
            </th>
            <th>
              <div class="th-text">Đơn vị tính</div>
              <div class="th-filter">
                <div>
                  <button class="btn-filter">
                    <span>∗</span>
                  </button>
                </div>
                <div style="width: 70px">
                  <base-input />
                </div>
              </div>
            </th>
            <th>
              <div class="th-text">Giá bán</div>
              <div class="th-filter">
                <div>
                  <button class="btn-filter">
                    <span>≤</span>
                  </button>
                </div>
                <div style="width: 80px">
                  <base-input />
                </div>
              </div>
            </th>
            <th>
              <div class="th-text">Thay đổi theo thời giá</div>
              <div class="th-filter text-align-left" style="width: 150px">
                <multiselect
                  v-model="valueTime"
                  :options="optionsYesNo"
                  placeholder=""
                  label="name"
                  track-by="name"
                  :showNoResults="false"
                  :showNoOptions="false"
                ></multiselect>
              </div>
            </th>
            <th>
              <div class="th-text">Điều chỉnh giá tự do</div>
              <div class="th-filter text-align-left" style="width: 130px">
                <multiselect
                  v-model="valuePrice"
                  :options="optionsYesNo"
                  placeholder=""
                  label="name"
                  track-by="name"
                  :showNoResults="false"
                  :showNoOptions="false"
                ></multiselect>
              </div>
            </th>
            <th>
              <div class="th-text">Định lượng NVL</div>
              <div class="th-filter text-align-left" style="width: 130px">
                <multiselect
                  v-model="valueNVL"
                  :options="optionsNVL"
                  placeholder=""
                  label="name"
                  track-by="name"
                  :showNoResults="false"
                  :showNoOptions="false"
                ></multiselect>
              </div>
            </th>
            <th>
              <div class="th-text">Hiển thị trên thực đơn</div>
              <div class="th-filter text-align-left" style="width: 130px">
                <multiselect
                  v-model="valueShowInMenus"
                  :options="optionsYesNo"
                  placeholder=""
                  label="name"
                  track-by="name"
                  :showNoResults="false"
                  :showNoOptions="false"
                ></multiselect>
              </div>
            </th>
            <th>
              <div class="th-text">Ngừng bán</div>
              <div class="th-filter text-align-left" style="width: 130px">
                <multiselect
                  v-model="valueStopSell"
                  :options="optionsYesNo"
                  placeholder=""
                  label="name"
                  track-by="name"
                  :showNoResults="false"
                  :showNoOptions="false"
                ></multiselect>
              </div>
            </th>
          </thead>
          <tbody>
            <tr
              v-for="menu in menus"
              :key="menu.menuID"
              :id="menu.menuID"
              @click="selectedRow(menu)"
              @dblclick="showDetailMenu(menu)"
              :class="{ trActive: menu.menuID == trActive }"
            >
              <td>Món ăn</td>
              <td>{{ menu.menuCode }}</td>
              <td>{{ menu.menuName }}</td>
              <td>{{ menu.menuGroup }}</td>
              <td>{{ menu.unit }}</td>
              <td class="text-align-right">
                {{ formatCash(menu.price.toString()) }}
              </td>
              <td class="text-align-center">
                <base-checkbox :disabled="true" />
              </td>
              <td class="text-align-center">
                <base-checkbox :disabled="true" />
              </td>
              <td>Chưa thiết lập</td>
              <td class="text-align-center">
                <base-checkbox
                  :disabled="true"
                  inputValue="true"
                  v-model="menu.showInMenus"
                />
              </td>
              <td class="text-align-center">
                <base-checkbox :disabled="true" />
              </td>
            </tr>
          </tbody>
        </table>
      </div>

      <div class="pagination">
        <div class="pagination-left">
          <div style="padding: 4px">
            <base-icon icon="firstPage" />
          </div>
          <div style="padding: 4px">
            <base-icon icon="prevPage" />
          </div>
          <div
            style="border-left: 1px solid #ccc; height: 16px; margin: 4px"
          ></div>
          <div style="padding: 0 8px">Trang</div>
          <div style="width: 42px">
            <base-input type="number" v-model="pageIndex" />
          </div>
          <div>trên {{ totalPages }}</div>
          <div
            style="border-left: 1px solid #ccc; height: 16px; margin: 4px 8px"
          ></div>
          <div style="padding: 4px">
            <base-icon icon="nextPage" />
          </div>
          <div style="padding: 4px">
            <base-icon icon="lastPage" />
          </div>
          <div
            style="border-left: 1px solid #ccc; height: 16px; margin: 4px 8px"
          ></div>
          <div style="padding: 4px">
            <base-icon icon="refreshPage" />
          </div>
          <div
            style="border-left: 1px solid #ccc; height: 16px; margin: 4px"
          ></div>
          <div class="cbo-pageSize" style="width: 60px; margin-left: 28px">
            <multiselect
              v-model="valuePageSize"
              :options="pageSizeSelect"
              label="name"
              track-by="name"
              :searchable="false"
              :allow-empty="false"
              class="pagesize"
            ></multiselect>
          </div>
        </div>

        <div class="txtTotal">
          Hiển thị 1 - {{ totalShow }} trên {{ totalRecords }} kết quả
        </div>
      </div>
    </div>

    <menu-detail
      v-if="isShowDetail"
      :menuID="menuID"
      :editMode="editMode"
      @closeForm="closeFormDetail"
      @loadData="loadData"
    />

    <base-dialog
      v-if="isShowDialogDel"
      icon="question"
      :text="msgConfirmDel"
      confirm
      @yes="deleteMenu()"
      @no="isShowDialogDel = false"
    />

    <base-loading v-if="isShowLoading" />
  </div>
</template>

<script>
import BaseButton from "../../components/base/BaseButton.vue";
import Multiselect from "vue-multiselect";
import axios from "axios";
import BaseInput from "../../components/base/BaseInput.vue";
import BaseCheckbox from "../../components/base/BaseCheckbox.vue";
import BaseIcon from "../../components/base/BaseIcon.vue";
import MenuDetail from "./MenuDetail.vue";
import BaseDialog from "../../components/base/BaseDialog.vue";
import BaseLoading from "../../components/base/BaseLoading.vue";
export default {
  components: {
    BaseButton,
    BaseInput,
    Multiselect,
    BaseCheckbox,
    BaseIcon,
    MenuDetail,
    BaseDialog,
    BaseLoading,
  },
  data: () => ({
    trActive: null, // Selected row
    menus: [], // Danh sách thực đơn
    menuID: null,
    editMode: null,
    // Multiselect Loại món
    valueType: { name: "Tất cả" },
    optionsType: [
      { name: "Tất cả" },
      { name: "Món ăn" },
      { name: "Đồ uống" },
      { name: "Mặt hàng khác" },
    ],
    // Option Không - Có
    optionsYesNo: [{ name: "Không" }, { name: "Có" }],
    valueTime: {}, // Value multiselect thay đổi theo thời giá
    valuePrice: {}, // Value multiselect thay đổi theo thời giá
    // Multiselect định lượng NVL
    valueNVL: {},
    optionsNVL: [{ name: "Đã thiết lập" }, { name: "Chưa thiết lập" }],
    valueShowInMenus: {}, // Value multiselect hiển thị trên thực đơn
    valueStopSell: { name: "Không" }, // Value multiselect ngừng bán
    // Multiselect số bản ghi trên trang
    pageSizeSelect: [
      { name: "25", value: 25 },
      { name: "50", value: 50 },
      { name: "100", value: 100 },
    ],
    valuePageSize: { name: "25", value: 100 },
    // Multiselect operator
    optionsOperator: [
      {name: "*: Chứa", value: 0},
      {name: "*: Bằng", value: 1},
    ],
    isShowDetail: false, // ẩn-hiện form chi tiết thực đơn
    isShowDialogDel: false, // ẩn hiện dialog xác nhận xóa
    msgConfirmDel: "", // thông báo xác nhận xóa
    menuName: null,
    menuCode: null,
    isShowLoading: false, // ẩn hiện loading
    filterObj: {
      conditions: [
        {
          property: null,
          value: null,
          operator: null,
        },
      ],
      sorters: [
        {
          property: null,
          order: null,
        },
      ],
      pageSize: 25,
      pageIndex: 1,
    },
    totalRecords: 0,
    totalPages: 0,
    totalShow: 0,
    pageIndex: 1,
  }),
  created() {
    this.filterObj = {
      sorters:[
        {
          property: "CreatedDate",
          order: 1
        }
      ],
      pageSize: 25,
      pageIndex: 1,
    };
    this.getMenus();
  },
  watch: {
    filterObj() {
      this.getMenus();
    },
    valuePageSize() {
      this.filterObj = {
        pageSize: this.valuePageSize.name,
        pageIndex: 1,
      };
    },
    pageIndex(){
      this.filterObj = {
        pageSize: this.valuePageSize.name,
        pageIndex: this.pageIndex,
      };
    }
  },
  methods: {
    /**
     - Load lại dữ liệu
     - Author: LQTrung(28/2/2022)
     */
    loadData(checkLoad) {
      if (checkLoad) {
        this.getMenus();
      }
    },
    /**
     - Lấy dữ liệu danh sách thực đơn
     - Author: LQTrung(28/2/2022)
     */
    getMenus() {
      var me = this;
      axios.post("menus/filter", this.filterObj).then(function (res) {
        console.log(res);
        me.showLoading();
        me.menus = res.data.data;
        me.totalRecords = res.data.totalRecords;
        me.totalPages = res.data.totalPages;
        me.totalShow = res.data.data.length;
        me.menuID = null;
        me.autoSelectRowFirst();
      });
    },
    /**
     - Click chọn dòng trong danh sách thực đơn
     - Author: LQTrung(28/2/2022)
     */
    selectedRow(menu) {
      this.trActive = menu.menuID;
      this.menuID = menu.menuID;
      this.menuCode = menu.menuCode;
      this.menuName = menu.menuName;
    },
    /**
     - Chọn dòng đầu tiền trong danh sách
     - Author: LQTrung(28/2/2022)
     */
    autoSelectRowFirst() {
      if (this.menus.length > 0) {
        if (!this.menuID) {
          this.menuID = this.menus[0].menuID;
          this.menuName = this.menus[0].menuName;
          this.menuCode = this.menus[0].menuCode;
          this.trActive = this.menuID;
        }
      }
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
     - Button thêm thực đơn
     - Author: LQTrung(28/2/2022)
     */
    btnAdd() {
      this.menuID = null;
      this.editMode = 0;
      this.isShowDetail = true;
    },
    /**
     - Button nhân bản
     - Author: LQTrung(28/2/2022)
     */
    btnClone() {
      this.isShowDetail = true;
      this.editMode = 0;
    },
    /**
     - Button sửa thực đơn
     - Author: LQTrung(28/2/2022)
     */
    btnEdit() {
      this.isShowDetail = true;
      this.editMode = 1;
    },
    /**
     - Button xóa thực đơn
     - Author: LQTrung(28/2/2022)
     */
    btnDelete() {
      this.isShowDialogDel = true;
      this.msgConfirmDel = `Bạn có chắc chắn muốn xóa món <<${this.menuCode} - ${this.menuName}>> không?`;
    },
    /**
     - Xóa một thực đơn
     - Author: LQTrung(28/2/2022)
     */
    deleteMenu() {
      var me = this;
      axios
        .delete(`menus/${this.menuID}`)
        .then(function (res) {
          console.log(res);
          me.isShowDialogDel = false;
          me.showLoading();
          me.getMenus();
        })
        .catch(function (err) {
          console.log(err);
        });
    },
    /**
     - Đóng form chi tiết thực đơn
     - Author: LQTrung(28/2/2022)
     */
    closeFormDetail(value) {
      if (value) {
        this.isShowDetail = false;
        this.autoSelectRowFirst();
      }
    },
    /**
     - Mở form chi tiết thực đơn để sửa
     - Author: LQTrung(28/2/2022)
     */
    showDetailMenu(menu) {
      this.isShowDetail = true;
      this.menuID = menu.menuID;
      this.editMode = 1;
    },
    /**
     - Hiện loading
     - Author: LQTrung(28/2/2022)
     */
    showLoading() {
      this.isShowLoading = true;
      setTimeout(() => {
        this.isShowLoading = false;
      }, 500);
    },
  },
};
</script>

<style scoped src="../../assets/css/view/menu.css"></style>
