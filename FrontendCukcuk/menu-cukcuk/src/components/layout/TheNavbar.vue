<template>
    <div class="navbar">
        <div class="menu-left">
            <ul>
                <li class="li-active">
                    <router-link to="/">
                        <div class="li-icon">
                        <div class="menu-food">
                        </div>
                    </div>
                    <div class="li-text">
                        <span>Thực đơn</span>
                    </div>
                    </router-link>
                </li>
            </ul>
        </div>
    </div>
</template>

<style scoped>
.navbar{
    width: 230px;
    background-color: #0973b9;
    color: #fff;
    height: calc(100vh - 55px);
}
.menu-left ul li{
    height: 38px;
    list-style-type: none;
    width: 100%;
    background-color: #0973b9;
    border-top: 1px solid #016087;
    border-bottom: 1px solid #016087;
}
.menu-left ul li a{
    display: flex;
    color: #fff;
}
.li-icon{
    padding: 5px 8px 5px 12px;
}
.li-text{
    line-height: 36px;
    width: 100%;
}
.menu-food{
    width: 24px;
    height: 24px;
    background-image: url('../../assets/imgs/vector-smart-object.png');
}
.li-active{
    background-color: #0199fc !important;
}
</style>