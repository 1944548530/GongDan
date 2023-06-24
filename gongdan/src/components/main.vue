<template>
    <div>
        <el-container>
            <div>
                <el-header height="60px">
                    工单查询
                </el-header>
            </div>
            <el-main>
                <div class="center">
                    <el-tabs type="border-card" @tab-click="tabClick">
                        <el-tab-pane label="半成品全检">
                            <banquanjian @QuanjInfo="GetQuanjInfo"/>
                        </el-tab-pane>
                        <el-tab-pane label="滚圆">
                            <gunyuan @GunyInfo="GetOtherInfo"/>
                        </el-tab-pane>
                        <el-tab-pane label="清洗">
                            <qingxi @QingxiInfo="GetOtherInfo"/>
                        </el-tab-pane>
                        <el-tab-pane label="预检">
                            <yujian @YujianInfo="GetOtherInfo"/>
                        </el-tab-pane>
                        <el-tab-pane label="裸片性能">
                            <xingneng @LuoXNInfo="GetOtherInfo"/>
                        </el-tab-pane>
                        <el-tab-pane label="初检">
                            <chujian @ChuJianInfo="GetOtherInfo"/>
                        </el-tab-pane>
                        <el-tab-pane label="套圈性能">
                            <fjxingneng @TaoXNInfo="GetOtherInfo"/>
                        </el-tab-pane>
                        <el-tab-pane label="复检">
                            <fujian @FuJianInfo="GetOtherInfo"/>
                        </el-tab-pane>
                        <el-tab-pane label="工单查询">
                            <chaxun @ChaXun="GetInfoBySn"/>
                        </el-tab-pane>
                    </el-tabs>

                    <el-card v-show="allShow">
                        <el-row>
                            <el-col :span="20">
                                <span class="inpTitle">工单号:</span>
                                <el-input style="width:180px;" v-model="gongdanS" placeholder="请输入工单"></el-input>&emsp;
                                <span class="inpTitle">日期:</span>
                                <el-date-picker v-model="dateS" style="width:180px;" type="date" value-format="yyyy-MM-dd" placeholder="选择日期"></el-date-picker>～
                                <el-date-picker v-model="dateSEnd" style="width:180px;" type="date" value-format="yyyy-MM-dd" placeholder="选择日期"></el-date-picker>
                                &emsp;&emsp;
                                <el-button type="primary" icon="el-icon-search" size="medium" @click="gongdanBtn">查询</el-button>&emsp;
                                <el-button type="primary" icon="el-icon-download" size="medium" @click="excelExport">导出</el-button>
                            </el-col>
                        </el-row>

                        <el-table :data="infoList" border stripe style="width: 100%;margin-left:2%;" :header-cell-style="{background:'#f5f7fa'}">
                            <el-table-column type="index"></el-table-column>
                            <el-table-column label="日期" align="center" header-align="center" width="90px" prop="date"></el-table-column>
                            <el-table-column label="工单号" align="center" header-align="center" width="120px" prop="snNum"></el-table-column>
                            <el-table-column label="工序" align="center" header-align="center" width="80px" prop="modular"></el-table-column>
                            <el-table-column label="品名" align="center" header-align="center" width="110px" prop="prodName"></el-table-column>
                            <el-table-column label="成品料号" align="center" header-align="center" width="110px" prop="item"></el-table-column>
                            <el-table-column label="尺寸" align="center" header-align="center" width="80px" prop="size"></el-table-column>
                            <el-table-column label="炉号" align="center" header-align="center" width="130px" prop="potNum"></el-table-column>
                            <el-table-column label="工单总量" align="center" header-align="center" width="80px" prop="totalAmount"></el-table-column>
                            <el-table-column label="剩余数量" align="center" header-align="center" width="80px" prop="leftNum"></el-table-column>
                            <el-table-column label="不良总量" align="center" header-align="center" width="80px" prop="erroTotal"></el-table-column>
                            <el-table-column label="前工序良品总量" align="center" header-align="center" width="80px" prop="lastProcOKNum"></el-table-column>
                            <el-table-column label="总不良率" align="center" header-align="center" width="80px" prop="erroTotalPer"></el-table-column>
                            <el-table-column label="投入数量" align="center" header-align="center" width="80px" prop="inputAmount"></el-table-column>
                            <el-table-column label="良品数量" align="center" header-align="center" width="80px" prop="opAmount"></el-table-column>
                            <el-table-column label="不良现象" align="center" header-align="center" width="80px" prop="erroNum"></el-table-column>
                            <el-table-column label="不良率" align="center" header-align="center" width="80px" prop="erroPer"></el-table-column>
                            <el-table-column label="不良现象明细" align="left" header-align="center" prop="erroDetail"></el-table-column>
                            <el-table-column label="烘烤" align="center" header-align="center" prop="hongkao"></el-table-column>
                            <el-table-column label="操作" align="center" width="150px" header-align="center" >
                                <template slot-scope="scope">
                                    <!-- 修改按钮 -->
                                    <el-tooltip
                                        :enterable="false"
                                        class="item"
                                        content="修改"
                                        effect="dark"
                                        placement="top"
                                    >
                                        <el-button
                                            @click="updateInfo(scope.row)"
                                            circle
                                            icon="el-icon-edit"
                                            size="mini"
                                            type="primary"
                                        ></el-button>
                                    </el-tooltip>
                                    <!-- 删除按钮 -->
                                    <el-tooltip
                                        :enterable="false"
                                        class="item"
                                        content="删除"
                                        effect="dark"
                                        placement="top"
                                    >
                                        <el-button
                                            @click="deleteInfo(scope.row)"
                                            circle
                                            icon="el-icon-delete"
                                            size="mini"
                                            type="primary"
                                        ></el-button>
                                    </el-tooltip>
                                </template>
                            </el-table-column>
                        </el-table>
                        <el-pagination
                                :current-page="pageNum"
                                :page-size="pagesize"
                                :page-sizes="[5,10]"
                                :total="total"
                                @current-change="handleCurrentChange"
                                @size-change="handleSizeChange"
                                background
                                layout="prev, pager, next, jumper, sizes, total"
                        ></el-pagination>
                    </el-card>

                    <el-card v-show="QJshow">
                        <el-row>
                            <el-col :span="20">
                                <span class="inpTitle">炉号:</span>
                                <el-input style="width:180px;" v-model="QJluhaoS" placeholder="请输入炉号"></el-input>&emsp;
                                <span class="inpTitle">日期:</span>
                                <el-date-picker v-model="QJdateS" value-format="yyyy-MM-dd" style="width:180px;" type="date" placeholder="选择日期"></el-date-picker>～
                                <el-date-picker v-model="QJdateSEnd" value-format="yyyy-MM-dd" style="width:180px;" type="date" placeholder="选择日期"></el-date-picker>
                                &emsp;&emsp;
                                <el-button type="primary" icon="el-icon-search" size="medium" @click="QJgongdanBtn">查询</el-button>&emsp;
                                <el-button type="primary" icon="el-icon-download" size="medium" @click="QJexcelExport">导出</el-button>
                            </el-col>
                        </el-row>

                        <el-table :data="QJinfoList" border stripe style="width: 97%;margin-left:4%;" :header-cell-style="{background:'#f5f7fa'}">
                            <el-table-column type="index"></el-table-column>
                            <el-table-column label="日期" align="center" header-align="center" width="90px" prop="date"></el-table-column>
                            <el-table-column label="品名" align="center" header-align="center" width="120px" prop="prodName"></el-table-column>
                            <el-table-column label="成品料号" align="center" header-align="center" width="120px" prop="item"></el-table-column>
                            <el-table-column label="尺寸" align="center" header-align="center" width="100px" prop="size"></el-table-column>
                            <el-table-column label="炉号" align="center" header-align="center" width="140px" prop="potNum"></el-table-column>
                            <el-table-column label="测试数量" align="center" header-align="center" width="100px" prop="totalAmount"></el-table-column>
                            <el-table-column label="不良数量" align="center" header-align="center" width="100px" prop="badNum"></el-table-column>
                            <el-table-column label="投入数量" align="center" header-align="center" width="100px" prop="inputAmount"></el-table-column>
                            <el-table-column label="良品数量" align="center" header-align="center" width="100px" prop="opAmount"></el-table-column>
                            <el-table-column label="不良现象" align="center" header-align="center" width="100px" prop="erroNum"></el-table-column>
                            <el-table-column label="不良率" align="center" header-align="center" width="100px" prop="erroPer"></el-table-column>
                            <el-table-column label="不良现象明细" align="left" header-align="center" prop="erroDetail"></el-table-column>
                            <el-table-column label="操作" align="center" header-align="center" width="150px">
                                <template slot-scope="scope">
                                    <!-- 修改按钮 -->
                                    <el-tooltip
                                        :enterable="false"
                                        class="item"
                                        content="修改"
                                        effect="dark"
                                        placement="top"
                                    >
                                        <el-button
                                            @click="QJupdateInfo(scope.row)"
                                            circle
                                            icon="el-icon-edit"
                                            size="mini"
                                            type="primary"
                                        ></el-button>
                                    </el-tooltip>
                                    <!-- 删除按钮 -->
                                    <el-tooltip
                                        :enterable="false"
                                        class="item"
                                        content="删除"
                                        effect="dark"
                                        placement="top"
                                    >
                                        <el-button
                                            @click="QJdeleteInfo(scope.row)"
                                            circle
                                            icon="el-icon-delete"
                                            size="mini"
                                            type="primary"
                                        ></el-button>
                                    </el-tooltip>
                                </template>
                            </el-table-column>
                        </el-table>
                        <el-pagination
                                :current-page="QJpageNum"
                                :page-size="QJpagesize"
                                :page-sizes="[5,10]"
                                :total="QJtotal"
                                @current-change="QJhandleCurrentChange"
                                @size-change="QJhandleSizeChange"
                                background
                                layout="prev, pager, next, jumper, sizes, total"
                        ></el-pagination>
                    </el-card>
                </div>
            </el-main>
        </el-container>
        <!--半成品全检信息修改对话框-->
        <el-dialog
            :visible.sync="QJeidtDialogVisible"
            @close="QJcloseEditDialog"
            title="信息修改"
            width="35%"
            class="abow_dialog"
            :close-on-click-modal="false"
        >
            <div class="dialogDiv">
                <el-form style="margin-left:50px;" label-width="20%" ref="formEditRef" v-model="QJeditForm" label-position="left">
                    <el-form-item label="日期" prop="dateUp">
                        <el-date-picker 
                            style="width:180px;"
                            size="small"
                            v-model="QJeditForm.dateUp"
                            value-format="yyyy-MM-dd"
                            placeholder="选择日期"
                        >
                        </el-date-picker>
                    </el-form-item>
                    <el-form-item label="品名" prop="shiftUp">
                        <el-input style="width:180px;" v-model="QJeditForm.pinMing" placeholder="请输入内容"></el-input>
                    </el-form-item>
                    <el-form-item label="成品料号" prop="lineUp">
                        <el-input style="width:180px;" v-model="QJeditForm.item" placeholder="请输入内容" @change="QJitemChange"></el-input>
                        <span style="color:red;font-size:10px;" v-show="QJitemErrShow">格式不对</span>
                    </el-form-item>
                    <el-form-item label="尺寸" prop="modelUp">
                        <el-input style="width:180px;" v-model="QJeditForm.size" placeholder="请输入内容"></el-input>
                    </el-form-item>
                    <el-form-item label="炉号" prop="rejNumUp">
                        <el-input style="width:180px;" v-model="QJeditForm.luhao" placeholder="请输入内容" @change="QJluhaoChange"></el-input>
                        <span style="color:red;font-size:10px;" v-show="QJluhaoErrShow">格式不对</span>
                    </el-form-item>
                    <el-form-item label="测试数量" prop="holdNumUp">
                        <el-input style="width:180px;" v-model="QJeditForm.testNum" placeholder="请输入数字"></el-input>
                    </el-form-item>
                    <el-form-item label="不良数量" prop="respDepUp">
                        <el-input style="width:180px;" v-model="QJeditForm.badNum" placeholder="请输入数字"></el-input>
                    </el-form-item>
                    <el-form-item label="产出数量" prop="respDepUp">
                        <el-input style="width:180px;" v-model="QJeditForm.opAmount" placeholder="请输入数字"></el-input>
                    </el-form-item>
                    <el-form-item label="短需烘烤" prop="corpUp">
                        <el-input style="width:180px;" v-model="QJeditForm.QJhkNum" placeholder="请输入数字"></el-input>
                    </el-form-item>
                    <el-form-item label="其他" prop="feedReasonUp">
                        <el-input style="width:180px;" v-model="QJeditForm.QJqtNum" placeholder="请输入数字"></el-input>
                    </el-form-item>
                    <el-form-item label="空白" prop="feedReasonUp">
                        <el-input style="width:100px;" v-model="QJeditForm.QJkbName" placeholder="不良名称"></el-input>
                        <el-input style="width:150px;" v-model="QJeditForm.QJkbNum" placeholder="请输入数字"></el-input>
                    </el-form-item>
                </el-form>
            </div>
            
            <div slot="footer" class="dialog-footer">
                <el-button @click="QJeidtDialogVisible = false">取 消</el-button>
                <el-button type="primary" @click="QJinfoUp">确 定</el-button>
            </div>
        </el-dialog>

        <!--其他信息修改对话框-->
        <el-dialog
            :visible.sync="GYeidtDialogVisible"
            @close="GYcloseEditDialog"
            title="信息修改"
            width="35%"
            class="abow_dialog"
            :close-on-click-modal="false"
        >
            <div class="dialogDiv">
                <el-form style="margin-left:50px;" label-width="20%" ref="GYformEditRef" v-model="GYeditForm" label-position="left">
                    <el-form-item label="日期" prop="dateUp">
                        <el-date-picker 
                            style="width:180px;"
                            size="small"
                            v-model="GYeditForm.dateUp"
                            value-format="yyyy-MM-dd"
                            placeholder="选择日期"
                        >
                        </el-date-picker>
                    </el-form-item>
                    <el-form-item label="品名" prop="shiftUp">
                        <el-input style="width:180px;" v-model="GYeditForm.pinMing" placeholder="请输入内容"></el-input>
                    </el-form-item>
                    <el-form-item label="成品料号" prop="lineUp">
                        <el-input style="width:180px;" v-model="GYeditForm.item" placeholder="请输入内容" @change="QJitemChange"></el-input>
                        <span style="color:red;font-size:10px;" v-show="QJitemErrShow">格式不对</span>
                    </el-form-item>
                    <el-form-item label="尺寸" prop="modelUp">
                        <el-input style="width:180px;" v-model="GYeditForm.size" placeholder="请输入内容"></el-input>
                    </el-form-item>
                    <el-form-item label="炉号" prop="rejNumUp">
                        <el-input style="width:180px;" v-model="GYeditForm.luhao" placeholder="请输入内容" @change="QJluhaoChange"></el-input>
                        <span style="color:red;font-size:10px;" v-show="QJluhaoErrShow">格式不对</span>
                    </el-form-item>
                    <el-form-item label="工单号" prop="holdNumUp">
                        <el-input style="width:180px;" v-model="GYeditForm.gongdan" placeholder="请输入数字"></el-input>
                    </el-form-item>
                    <el-form-item label="工单总量" prop="holdNumUp">
                        <el-input style="width:180px;" v-model="GYeditForm.gdAmount" placeholder="请输入数字"></el-input>
                    </el-form-item>
                    <el-form-item label="产出数量" prop="holdNumUp">
                        <el-input style="width:180px;" v-model="GYeditForm.outputNum" placeholder="请输入数字"></el-input>
                    </el-form-item>
                    <div v-if="this.modularNow == '滚圆'">
                        <el-form-item label="滚坏" prop="corpUp">
                            <el-input style="width:180px;" v-model="GYeditForm.ghNum" placeholder="请输入数字"></el-input>
                        </el-form-item>
                        <el-form-item label="其他" prop="feedReasonUp">
                            <el-input style="width:180px;" v-model="GYeditForm.qtNum" placeholder="请输入数字"></el-input>
                        </el-form-item>
                        <el-form-item label="空白" prop="feedReasonUp">
                            <el-input style="width:100px;" v-model="GYeditForm.GYkbName" placeholder="不良名称"></el-input>
                            <el-input style="width:150px;" v-model="GYeditForm.GYkbNum" placeholder="请输入数字"></el-input>
                        </el-form-item>
                    </div>
                    <div v-else-if=" (this.modularNow == '预检') || (this.modularNow == '初检') || (this.modularNow == '复检')">
                        <el-form-item label="刮伤" prop="corpUp">
                            <el-input style="width:180px;" v-model="GYeditForm.gsNum" placeholder="请输入数字"></el-input>
                        </el-form-item>
                        <el-form-item label="麻点" prop="feedReasonUp">
                            <el-input style="width:180px;" v-model="GYeditForm.mdNum" placeholder="请输入数字"></el-input>
                        </el-form-item>
                        <el-form-item label="崩边" prop="feedReasonUp">
                            <el-input style="width:180px;" v-model="GYeditForm.bbNum" placeholder="请输入数字"></el-input>
                        </el-form-item>
                        <el-form-item label="花边" prop="feedReasonUp">
                            <el-input style="width:180px;" v-model="GYeditForm.huabNum" placeholder="请输入数字"></el-input>
                        </el-form-item>
                        <el-form-item label="脱点" prop="feedReasonUp">
                            <el-input style="width:180px;" v-model="GYeditForm.tuodNum" placeholder="请输入数字"></el-input>
                        </el-form-item>
                        <el-form-item label="其他" prop="feedReasonUp">
                            <el-input style="width:180px;" v-model="GYeditForm.qtNum" placeholder="请输入数字"></el-input>
                        </el-form-item>
                        <el-form-item label="空白" prop="feedReasonUp">
                            <el-input style="width:100px;" v-model="GYeditForm.GYkbName" placeholder="不良名称"></el-input>
                            <el-input style="width:150px;" v-model="GYeditForm.GYkbNum" placeholder="请输入数字"></el-input>
                        </el-form-item>
                    </div>
                    <div v-else-if="(modularNow == '裸片性能') || (modularNow == '套圈性能')">
                        <el-form-item label="短需烘烤" prop="corpUp">
                            <el-input style="width:180px;" v-model="GYeditForm.hkNum" placeholder="请输入数字"></el-input>
                        </el-form-item>
                        <el-form-item label="其他" prop="feedReasonUp">
                            <el-input style="width:180px;" v-model="GYeditForm.qtNum" placeholder="请输入数字"></el-input>
                        </el-form-item>
                        <el-form-item label="空白" prop="feedReasonUp">
                            <el-input style="width:100px;" v-model="GYeditForm.GYkbName" placeholder="不良名称"></el-input>
                            <el-input style="width:150px;" v-model="GYeditForm.GYkbNum" placeholder="请输入数字"></el-input>
                        </el-form-item>
                    </div>
                    <div v-else>
                        <el-form-item label="刮伤" prop="corpUp">
                            <el-input style="width:180px;" v-model="GYeditForm.gsNum" placeholder="请输入数字"></el-input>
                        </el-form-item>
                        <el-form-item label="丢片" prop="corpUp">
                            <el-input style="width:180px;" v-model="GYeditForm.dpNum" placeholder="请输入数字"></el-input>
                        </el-form-item>
                        <el-form-item label="其他" prop="feedReasonUp">
                            <el-input style="width:180px;" v-model="GYeditForm.qtNum" placeholder="请输入数字"></el-input>
                        </el-form-item>
                        <el-form-item label="空白" prop="feedReasonUp">
                            <el-input style="width:100px;" v-model="GYeditForm.GYkbName" placeholder="不良名称"></el-input>
                            <el-input style="width:150px;" v-model="GYeditForm.GYkbNum" placeholder="请输入数字"></el-input>
                        </el-form-item>
                    </div>
                </el-form>
            </div>
            
            <div slot="footer" class="dialog-footer">
                <el-button @click="GYeidtDialogVisible = false">取 消</el-button>
                <el-button type="primary" @click="GYinfoUp">确 定</el-button>
            </div>
        </el-dialog>
    </div>
</template>

<script>
    import gunyuan from './page/gunyuan'
    import qingxi from './page/qingxi'
    import yujian from './page/yujian'
    import xingneng from './page/xingneng'
    import chujian from './page/chujian'
    import fjxingneng from './page/fjxingneng'
    import fujian from './page/fujian'
    import banquanjian from './page/banquanjian'
    import chaxun from './page/chaxun'
    export default{
        name:"Main",
        components:{
            gunyuan,
            qingxi,
            yujian,
            xingneng,
            chujian,
            fjxingneng,
            fujian,
            banquanjian,
            chaxun
        },
        data: function(){
            return{
                gongdanS: '',
                infoList: [],
                QJinfoList: [],
                pageNum:1,
                pagesize: 10,
                allShow: false,
                QJshow: true,
                dateS: '',
                dateSEnd: '',
                total: 0,
                QJtotal: 0,
                QJpageNum: 1,
                QJpagesize: 5,
                QJluhaoS: '',
                QJdateS: '',
                QJdateSEnd: '',
                modularNow: '',
                QJeidtDialogVisible: false,
                QJitemErrShow: false,
                QJluhaoErrShow: false,
                GYeidtDialogVisible: false,
                GYeditForm: {
                    dateUp: '',
                    pinMing: '',
                    item: '',
                    size: '',
                    luhao: '',
                    gongdan: '',
                    gdAmount: '',
                    gdAmountOld:'',
                    outputNum: '',
                    ghNum: '',
                    qtNum: '',
                    GYkbName: '',
                    GYkbNum: '',
                    gsNum: '',
                    mdNum: '',
                    bbNum: '',
                    hkNum: '',
                    dateOld: '',
                    modular: '',
                    opAmountOld: '',
                    erroDetailOld: '',
                    dpNum: '',
                    huabNum: '',
                    tuodNum: ''
                },
                QJeditForm: {
                    dateUp: '',
                    pinMing: '',
                    item: '',
                    size: '',
                    luhao: '',
                    testNum: '',
                    erroNum: '',
                    badNum: '',
                    opAmount: '',
                    QJhkNum: '',
                    QJqtNum: '',
                    QJkbName: '',
                    QJkbNum: '',
                    luhaoOld: '',
                    dateOld: '',
                    modular: '',
                    erroDetailOld: '',
                    opAmountOld: ''
                }
            }
        },
        created: function(){
            var date = new Date()
            this.QJdateSEnd = this.getDateFormat(date)
            this.dateSEnd = this.getDateFormat(date)
            this.GetQuanjInfo()
            // this.GetOtherInfo()
        },
        methods:{
            GetInfoBySn: function(){
            },
            GYcloseEditDialog: function(){
                this.GYeidtDialogVisible = false
            },
            QJcloseEditDialog: function(){
                this.QJeidtDialogVisible = false
            },
            QJluhaoChange: function(){
                if(this.QJeditForm.luhao.length != 15){
                    this.QJluhaoErrShow = true
                }else{
                    this.QJluhaoErrShow = false
                }
            },
            QJitemChange: function(){
                if(this.QJeditForm.item.length != 7){
                    this.QJitemErrShow = true
                }else{
                    this.QJitemErrShow = false
                }
            },
            gongdanBtn: function(){
                this.GetOtherInfo(this.modularNow)
            },
            GYinfoUp: function(e){
                this.$http.get("GunyMain/OtherInfoUp",{
                    params: this.GYeditForm
                }).then(res => {
                    const result = res.data
                    if(result.code == 200){
                        this.$message({
                            showClose: true,
                            message: '修改成功',
                            type: 'success',
                            duration: 2000,
                        })
                        this.GYeditForm.GYkbName = ''
                        this.GYeditForm.GYkbNum = ''
                        this.GYeidtDialogVisible = false
                        this.GetOtherInfo(this.modularNow)
                    }else{
                        this.$message({
                            showClose: true,
                            message: result.message,
                            type: 'error',
                            duration: 2000,
                        })
                    }
                })
            },
            QJupdateInfo: function(e){
                this.QJeidtDialogVisible = true
                this.QJeditForm.dateUp = e.date
                this.QJeditForm.dateOld = e.date
                this.QJeditForm.pinMing = e.prodName
                this.QJeditForm.item = e.item
                this.QJeditForm.size = e.size
                this.QJeditForm.luhao = e.potNum
                this.QJeditForm.luhaoOld = e.potNum
                this.QJeditForm.testNum = e.totalAmount
                this.QJeditForm.badNum = e.badNum
                this.QJeditForm.opAmount = e.opAmount
                this.QJeditForm.erroDetailOld = e.erroDetail
                this.QJeditForm.opAmountOld = e.opAmount
                var str = e.erroDetail.replace("短需烘烤:","").replace("其他:","").replace(":",",")
                var strArr = str.split(",")
                this.QJeditForm.QJhkNum = strArr[0].trim()
                this.QJeditForm.QJqtNum = strArr[1].trim()
                if(strArr.length == 3){
                    var strArr1 = strArr[2].split(":")
                    this.QJeditForm.QJkbName = strArr1[0].trim()
                    this.QJeditForm.QJkbNum = strArr1[1].trim()
                }
            },
            QJinfoUp: function(){
                this.$http.get("QuanjMain/QuanjUp",{
                    params: this.QJeditForm
                }).then(res => {
                    const result = res.data
                    if(result.code == 200){
                        this.$message({
                            showClose: true,
                            message: '修改成功',
                            type: 'success',
                            duration: 2000,
                        })
                        
                        this.QJeidtDialogVisible = false
                        this.GetQuanjInfo()
                    }else{
                        this.$message({
                            showClose: true,
                            message: result.message,
                            type: 'error',
                            duration: 2000,
                        })
                    }
                })
            },
            updateInfo: function(e){
                this.GYeidtDialogVisible = true
                this.GYeditForm.dateUp = e.date
                this.GYeditForm.pinMing = e.prodName
                this.GYeditForm.item = e.item
                this.GYeditForm.size = e.size
                this.GYeditForm.luhao = e.potNum
                this.GYeditForm.gongdan = e.snNum
                this.GYeditForm.gdAmount = e.totalAmount
                this.GYeditForm.gdAmountOld = e.totalAmount
                this.GYeditForm.outputNum = e.opAmount
                this.GYeditForm.modular = this.modularNow
                this.GYeditForm.dateOld = e.date
                this.GYeditForm.opAmountOld = e.opAmount
                this.GYeditForm.erroDetailOld = e.erroDetail
                if(this.modularNow == '滚圆'){
                    var str = e.erroDetail.replace("滚坏:","").replace("其他:","")
                    var strArr = str.split(",")
                    
                    this.GYeditForm.ghNum = strArr[0].trim()
                    this.GYeditForm.qtNum = strArr[1].trim()
                    if(strArr.length == 3){
                        var strArr1 = strArr[2].split(":")
                        this.GYeditForm.GYkbName = strArr1[0].trim()
                        this.GYeditForm.GYkbNum = strArr1[1].trim()
                    }
                    
                }else if(this.modularNow == '预检' || this.modularNow == '初检' || this.modularNow == '复检'){
                    var str = e.erroDetail.replace("刮伤:","").replace("麻点:","").replace("崩边:","").replace("花边:","").replace("脱点:","").replace("其他:","")
                    var strArr = str.split(",")
                    console.log(strArr)
                    this.GYeditForm.gsNum = strArr[0].trim()
                    this.GYeditForm.mdNum = strArr[1].trim()
                    this.GYeditForm.bbNum = strArr[2].trim()
                    this.GYeditForm.huabNum = strArr[3].trim()
                    this.GYeditForm.tuodNum = strArr[4].trim()
                    this.GYeditForm.qtNum = strArr[5].trim()
                    if(strArr.length == 7){
                        var strArr1 = strArr[6].split(":")
                        this.GYeditForm.GYkbName = strArr1[0].trim()
                        this.GYeditForm.GYkbNum = strArr1[1].trim()
                    }
                }else if(this.modularNow == '裸片性能' || this.modularNow == '套圈性能'){
                    var str = e.erroDetail.replace("短需烘烤:","").replace("其他:","")
                    var strArr = str.split(",")
                    this.GYeditForm.hkNum = strArr[0].trim()
                    this.GYeditForm.qtNum = strArr[1].trim()
                    if(strArr.length == 3){
                        var strArr1 = strArr[2].split(":")
                        this.GYeditForm.GYkbName = strArr1[0].trim()
                        this.GYeditForm.GYkbNum = strArr1[1].trim()
                    }
                }else{
                    var str = e.erroDetail.replace("刮伤:","").replace("丢片:","").replace("其他:","")
                    var strArr = str.split(",")
                    this.GYeditForm.gsNum = strArr[0].trim()
                    this.GYeditForm.dpNum = strArr[1].trim()
                    this.GYeditForm.qtNum = strArr[2].trim()
                    if(strArr.length == 4){
                        var strArr1 = strArr[3].split(":")
                        this.GYeditForm.GYkbName = strArr1[0].trim()
                        this.GYeditForm.GYkbNum = strArr1[1].trim()
                    }
                }
                

            },
            QJdeleteInfo: function(e){
                this.$confirm('确定删除?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(async() => {
                    this.$http.get('QuanjMain/QuanjDel',{
                        params:{
                            date: e.date,
                            item: e.item,
                            potNum: e.potNum,
                            opAmount: e.opAmount, 
                            erroDetail: e.erroDetail
                        }
                    }).then(res => {
                        const result = res.data
                        if(result.code == 200){
                            this.GetQuanjInfo(this.modularNow)
                        }else{
                            this.$message({
                                showClose: true,
                                message: result.message,
                                type: 'error',
                                duration: 2000,
                            })
                        }
                    })
                })
                
            },
            deleteInfo: function(e){
                this.$confirm('确定删除?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(async() => {
                    this.$http.get('GunyMain/OtherInfoDel',{
                        params:{
                            date: e.date,
                            snNum: e.snNum,
                            modular: e.modular,
                            opAmount: e.opAmount, 
                            erroDetail: e.erroDetail 
                        }
                    }).then(res => {
                        const result = res.data
                        if(result.code == 200){
                            this.GetOtherInfo(this.modularNow)
                        }else{
                            this.$message({
                                showClose: true,
                                message: result.message,
                                type: 'error',
                                duration: 2000,
                            })
                        }
                    })
                })
                
            },
            excelExport: function(){
                this.$http.get("GunyMain/ExportExcel",
                {
                    params:{
                        snNum: this.gongdanS,
                        date: this.dateS,
                        dateEnd: this.dateSEnd,
                        modular: this.modularNow
                    },
                    responseType: 'blob',
                    headers:{
                        'Content-Type': 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
                    }
                }).then(res =>{
                    const resultData = res.data
                    const fileName = "工单" + ".xlsx"
                    this.downFile(resultData, fileName)
                })
            },
            QJgongdanBtn: function(){
                this.GetQuanjInfo()
            },
            QJexcelExport: function(){
                this.$http.get("QuanjMain/ExportExcel",
                {
                    params:{
                        potNum: this.QJluhaoS,
                        date: this.QJdateS,
                        dateEnd: this.QJdateSEnd
                    },
                    responseType: 'blob',
                    headers:{
                        'Content-Type': 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
                    }
                }).then(res =>{
                    const resultData = res.data
                    const fileName = "半成品全检工单" + ".xlsx"
                    this.downFile(resultData, fileName)
                })
            },
            tabClick: function(tab, event){
                this.pageNum = 1
                this.pagesize = 10
                if(tab.$options.propsData.label == "半成品全检"){
                    this.QJshow = true
                    this.allShow = false
                }else if(tab.$options.propsData.label == "工单查询"){
                    this.QJshow = false
                    this.allShow = false        
                }
                else{
                    this.QJshow = false
                    this.allShow = true
                    if(tab.$options.propsData.label == "滚圆"){
                        this.modularNow = "滚圆"
                        this.GetOtherInfo("滚圆")
                    }else if(tab.$options.propsData.label == "清洗"){
                        this.modularNow = "清洗"
                        this.GetOtherInfo("清洗")
                    }else if(tab.$options.propsData.label == "预检"){
                        this.modularNow = "预检"
                        this.GetOtherInfo("预检")
                    }else if(tab.$options.propsData.label == "裸片性能"){
                        this.modularNow = "裸片性能"
                        this.GetOtherInfo("裸片性能")
                    }else if(tab.$options.propsData.label == "初检"){
                        this.modularNow = "初检"
                        this.GetOtherInfo("初检")
                    }else if(tab.$options.propsData.label == "套圈性能"){
                        this.modularNow = "套圈性能"
                        this.GetOtherInfo("套圈性能")
                    }else{
                        this.modularNow = "复检"
                        this.GetOtherInfo("复检")
                    }
                }
            },
            QJgongdanBtn: function(){
                this.GetQuanjInfo()
            },
            GetOtherInfo: function(modular){
                this.$http.get('GunyMain/GetOtherInfo',{
                    params:{
                        snNum: this.gongdanS,
                        date: this.dateS,
                        dateEnd: this.dateSEnd,
                        pageNum: this.pageNum,
                        pagesize: this.pagesize,
                        modular: modular
                    }
                }).then(res => {
                    const result = res.data
                    this.infoList = result.rows
                    this.total = result.total
                })
            },
            GetQuanjInfo: function(){
                this.$http.get('QuanjMain/GetQuanjInfo',{
                    params:{
                        potNum: this.QJluhaoS,
                        date: this.QJdateS,
                        dateEnd: this.QJdateSEnd,
                        pageNum: this.QJpageNum,
                        pageSize: this.QJpagesize
                    }
                }).then(res => {
                    const result = res.data
                    console.log(result)
                    this.QJinfoList = result.rows
                    this.QJtotal = result.total
                })
            },
            handleCurrentChange: function(newPage){
                this.pageNum = newPage
                this.GetOtherInfo(this.modularNow)
            },
            handleSizeChange: function(newSize){
                this.pagesize = newSize
                this.GetOtherInfo(this.modularNow)
            },
            QJhandleCurrentChange: function(newPage){
                this.QJpageNum = newPage
                this.GetQuanjInfo()
            },
            QJhandleSizeChange: function(newSize){
                this.QJpagesize = newSize
                this.GetQuanjInfo()
            },
            downFile: function(data, fileName) {
                if (!data) {
                    return false;
                }
                this.$message({
                    showClose: true,
                    message: '信息导出完成，请您注意浏览器的下载管理器！',
                    type: 'success',
                    duration: 2000,
                })
                // 下面就是DOM操作 1.添加一个a标签 2.给a标签添加了属性 3.给他添加了点击事件(点击后移除)   
                const url = window.URL.createObjectURL(new Blob([data]),{type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'});
                const link = document.createElement("a");
                link.style.display = "none";
                link.href = url;
                link.setAttribute("download", fileName);
                document.body.appendChild(link);
                link.click();
                link.remove();
            },
            getDateFormat: function(Date){
                var Y = Date.getFullYear();
                var M = Date.getMonth() + 1;
                var D = Date.getDate();
                var times = Y + (M < 10 ? "-0" : "-") + M + (D < 10 ? "-0" : "-") + D;
                return times;
            }
        }
    }
</script>

<style>
    .el-main {
        background-color: #E9EEF3;
        color: #333;
        margin-top:60px;
        z-index: 0; 
        height:100%;
        /* height:885px; */
    }
    .el-header {
        background-color: #03a9f4;
        text-align: center;
        line-height: 60px;
        width:100%;
        font-size:25px;
        color:white;
        position:fixed;
        z-index: 1;
    }
    .center{
        border:1px solid #AAAAAA;
    }
    .el-tabs__item{
        width:100px;
        text-align: center;
        color:black;
    }
    .el-tabs--border-card>.el-tabs__header .el-tabs__item{
        color:#444444;
        font-weight: bold;
    }
    .procDiv{
        
        margin-top:10px;
        margin-bottom:10px;
    }
    .el-input{
        width:180px;
    }
    .el-col {
        color:#444444;
        height: 55px;
        line-height: 55px;
    }
    .el-row {
        margin-bottom: 20px;
        margin-left:50px;
    }
    /* .inpTitle{
        color:#444444;
        font-weight: bold;
    } */
    .el-card{
        margin-top:10px;
    }
    .el-pagination{
        margin-top:30px;
        margin-left:3%;
        margin-bottom: 50px;
    }
    .dialogDiv{
        height:500px;
        overflow: auto;
    }
</style>