<template>
    <div class="procDiv">
        <el-row>
            <el-col :span="6">
                <span class="inpTitle">工单号:</span>
                <el-input style="width:180px;" v-model="GYgongdan" placeholder="请输入内容" @change="snChange"></el-input>
                <span style="color:red;font-size:10px;" v-show="GYgongdErrShow">格式不对</span>
            </el-col>
            <el-col :span="6">
                <span class="inpTitle">品名:&emsp;&emsp;</span>
                <el-input style="width:180px;" v-model="GYpinMing" placeholder="请输入内容"></el-input>
            </el-col>
            <el-col :span="6">
                <span class="inpTitle">成品料号:</span>
                <el-input style="width:180px;" v-model="GYitem" placeholder="请输入内容" @change="GYitemChange"></el-input>
                <span style="color:red;font-size:10px;" v-show="GYitemErrShow">格式不对</span>
            </el-col>
            <el-col :span="6">
                <span class="inpTitle">尺寸:</span>
                <el-input style="width:180px;" v-model="GYsize" placeholder="请输入内容"></el-input>
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="6">
                <span class="inpTitle">炉号:&emsp;</span>
                <el-input style="width:180px;" v-model="GYluhao" placeholder="请输入内容" @change="GYluhaoChange"></el-input>
                <span style="color:red;font-size:10px;" v-show="GYluhaoErrShow">格式不对</span>
            </el-col>
            <el-col :span="6">
                <span class="inpTitle">工单总量:</span>
                    <el-input style="width:180px;" v-model="GYgdAmount" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="6">
                <span class="inpTitle">良品数量:</span>
                    <el-input style="width:180px;" v-model="GYoutputNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="6">
                <span class="inpTitle">滚坏:</span>
                <el-input style="width:180px;" v-model="GYghNum" placeholder="请输入数字"></el-input>
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="6">
                <span class="inpTitle">其他:&emsp;</span>
                <el-input style="width:180px;" v-model="GYotherNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="6">
                <el-input style="width:100px;" v-model="GYkbName" placeholder="不良名称"></el-input>
                <el-input style="width:150px;" v-model="GYkbNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="6">
                <el-select v-model="hongkao" placeholder="请选择">
                    <el-option
                        v-for="item in hongkaoOption"
                        :key="item.value"
                        :label="item.label"
                        :value="item.value"
                    >
                    </el-option>
                </el-select>
            </el-col>
            <el-col :span="6">
                <el-button @click="GYinfoSave" size="medium" type="primary">保存</el-button>
            </el-col>
        </el-row>
    </div>
</template>

<script>
    export default{
        data(){
            return{
                GYpinMing: '',
                GYitem: '',
                GYsize: '',
                GYluhao: '',
                GYgongdan: '',
                GYgdAmount: '',
                GYoutputNum: '',
                GYghNum: '',
                GYotherNum: '',
                GYkbNum: '',
                hongkao: '烘烤',
                GYitemErrShow: false,
                GYluhaoErrShow: false,
                GYgongdErrShow: false,
                GYkbName: '',
                hongkaoOption:[
                    {value: '烘烤', label: '烘烤'},
                    {value: '未烘烤', label: '未烘烤'}
                ],
            }
        },
        created: function(){
            //this.$emit('GunyInfo', '滚圆')
        },
        computed:{
            
        },
        methods:{
            GYitemChange: function(){
                if(this.GYitem.length != 7){
                    this.GYitemErrShow = true
                }else{
                    this.GYitemErrShow = false
                }
            },
            GYluhaoChange: function(){
                if(this.GYluhao.length != 15){
                    this.GYluhaoErrShow = true
                }else{
                    this.GYluhaoErrShow = false
                }
            },
            snChange: function(){
                if(this.GYgongdan.length != 12){
                    this.GYgongdErrShow = true
                }else{
                    this.GYgongdErrShow = false
                    this.$http.get('QingxiMain/GetInfoBySn', {
                        params:{
                            sn: this.GYgongdan
                        }
                    }).then(res => {
                        const result = res.data
                        if(result.code == 200){
                            const info = result.Data
                            this.GYpinMing = info.prodName
                            this.GYitem = info.item
                            this.GYsize = info.size
                            this.GYluhao = info.potNum
                            this.GYgdAmount = info.totalAmount
                        }else{
                            this.$message({
                                showClose: true,
                                message: result.message,
                                type: 'error',
                                duration: 2000,
                            })
                            this.GYpinMing = ''
                            this.GYitem = ''
                            this.GYsize = ''
                            this.GYluhao = ''
                            this.GYgdAmount = ''
                        }
                        
                    })
                }
                
            },
            GYinfoSave: function(){
                if(this.GYpinMing == "" || this.GYitem == "" || this.GYsize == "" || this.GYluhao == "" || this.GYgongdan == ""
                    || this.GYgdAmount == "" || this.GYoutputNum == "" || this.GYghNum == "" || this.GYotherNum == "" 
                    || this.GYitemErrShow == true || this.GYluhaoErrShow == true || this.GYgongdErrShow == true){
                    this.$message({
                        showClose: true,
                        message: "请确认输入项不能为空且格式正确",
                        type: 'error',
                        duration: 2000,
                    })
                }else{
                    this.$http.get('GunyMain/GunySave',{
                        params:{
                            prodName: this.GYpinMing,
                            item: this.GYitem,
                            size: this.GYsize,
                            potNum: this.GYluhao,
                            snNum: this.GYgongdan,
                            totalAmount: this.GYgdAmount,
                            opAmount: this.GYoutputNum,
                            erro1: this.GYghNum,
                            erro2: this.GYotherNum,
                            errBlankNum: this.GYkbNum,
                            errBlankName: this.GYkbName,
                            hongkao: this.hongkao    
                        }
                    }).then(res => {
                        const result = res.data
                        if(result.code == 200){
                            this.$message({
                                showClose: true,
                                message: '保存成功',
                                type: 'success',
                                duration: 2000,
                            })
                            this.$emit('GunyInfo', '滚圆')
                            this.GYpinMing = ''
                            this.GYitem = ''
                            this.GYsize = ''
                            this.GYluhao = ''
                            this.GYgongdan = ''
                            this.GYgdAmount = ''
                            this.GYoutputNum = ''
                            this.GYghNum = ''
                            this.GYotherNum = ''
                            this.GYkbNum = ''
                            this.GYkbName = ''
                        }else{
                            this.$message({
                                showClose: true,
                                message: result.message,
                                type: 'error',
                                duration: 2000,
                            })
                        }
                    })
                }
            }
            
        }
    }
</script>

<style>
  ::v-deep .el-input{
        width:180px;
    }  
    
</style>