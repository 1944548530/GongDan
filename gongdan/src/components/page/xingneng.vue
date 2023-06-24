<template>
    <div class="procDiv">
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">工单号:</span>
                <el-input style="width:180px;" v-model="XNgongdan" placeholder="请输入内容" @change="snChange"></el-input>
                
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">品名:&emsp;&emsp;</span>
                <el-input style="width:180px;" v-model="XNpinMing" :disabled="true" placeholder="请输入内容"></el-input>
                
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">成品料号:</span>
                <el-input style="width:180px;" v-model="XNitem" :disabled="true" placeholder="请输入内容"></el-input>
                
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">尺寸:&emsp;&emsp;</span>
                <el-input style="width:180px;" v-model="XNsize" :disabled="true" placeholder="请输入内容"></el-input>
                
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">炉号:</span>&emsp;
                <el-input style="width:180px;" v-model="XNluhao" :disabled="true" placeholder="请输入内容"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">工单总量:</span>
                    <el-input style="width:180px;" v-model="XNgdAmount" :disabled="true" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">良品数量:</span>
                    <el-input style="width:180px;" v-model="XNoutputNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">短需烘烤:</span>
                <el-input style="width:180px;" v-model="XNduanNum" placeholder="请输入数字"></el-input>
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">其他:</span>&emsp;
                <el-input style="width:180px;" v-model="XNqitaNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <el-input style="width:100px;" v-model="XNkongbName" placeholder="不良名称"></el-input>
                <el-input style="width:150px;" v-model="XNkongbNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <el-select v-model="hongkao" placeholder="请选择" disabled>
                    <el-option
                        v-for="item in hongkaoOption"
                        :key="item.value"
                        :label="item.label"
                        :value="item.value"
                    >
                    </el-option>
                </el-select>
            </el-col>
            <el-col :span="5">
                <el-button @click="XNinfoSave" size="medium" type="primary">保存</el-button>
            </el-col>
        </el-row>
    </div>
</template>

<script>
    export default{
        data(){
            return{
                XNpinMing: '',
                XNitem: '',
                XNsize: '',
                XNluhao: '',
                XNgongdan: '',
                XNgdAmount: '',
                XNoutputNum: '',
                XNduanNum: '',
                XNqitaNum: '',
                XNkongbNum: '',
                XNkongbName: '',
                hongkao: '烘烤',
                hongkaoOption:[
                    {value: '烘烤', label: '烘烤'},
                    {value: '未烘烤', label: '未烘烤'}
                ],
            }
        },
        computed:{
            
        },
        methods:{
            snChange: function(){
                this.$http.get('QingxiMain/GetInfoBySn', {
                    params:{
                        sn: this.XNgongdan
                    }
                }).then(res => {
                    const result = res.data
                    if(result.code == 200){
                        const info = result.Data
                        this.XNpinMing = info.prodName
                        this.XNitem = info.item
                        this.XNsize = info.size
                        this.XNluhao = info.potNum
                        this.XNgdAmount = info.totalAmount
                        this.hongkao = info.hongkao
                    }else{
                        this.$message({
                            showClose: true,
                            message: result.message,
                            type: 'error',
                            duration: 2000,
                        })
                        this.XNpinMing = ''
                        this.XNitem = ''
                        this.XNsize = ''
                        this.XNluhao = ''
                        this.XNgdAmount = ''
                    }
                    
                })
            },
            XNinfoSave: function(){
                if(this.XNpinMing == "" || this.XNitem == "" || this.XNsize == "" || this.XNluhao == ""
                    || this.XNgongdan == "" || this.XNgdAmount == "" || this.XNoutputNum == ""  
                    || this.XNduanNum == "" || this.XNqitaNum == ""){
                    this.$message({
                        showClose: true,
                        message: "输入项不能为空",
                        type: 'error',
                        duration: 2000,
                    })
                }else{
                    this.$http.get('XingNeng/LuoXNSave',{
                        params:{
                            prodName: this.XNpinMing,
                            item: this.XNitem,
                            size: this.XNsize,
                            potNum: this.XNluhao,
                            snNum: this.XNgongdan,
                            totalAmount: this.XNgdAmount,
                            opAmount: this.XNoutputNum,
                            erro1: this.XNduanNum,
                            erro2: this.XNqitaNum,
                            errBlankNum: this.XNkongbNum,
                            errBlankName: this.XNkongbName,
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
                            this.$emit('LuoXNInfo', '裸片性能')
                            this.XNpinMing = ''
                            this.XNitem = ''
                            this.XNsize = ''
                            this.XNluhao = ''
                            this.XNgongdan = ''
                            this.XNgdAmount = ''
                            this.XNoutputNum = ''
                            this.XNduanNum = ''
                            this.XNqitaNum = ''
                            this.XNkongbNum = ''
                            this.XNkongbName = ''
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