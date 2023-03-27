<template>
    <div>
        <myDialog :visible.sync="showDialog" :title="initParams.title" width="1000px" top="10vh">
            <el-form ref="form" :model="form" label-width="100px">
                
                
                
                
                
                <el-form-item label="合价">
                    <el-input v-model="form.CombinedPrice" />
                </el-form-item>
                
                
                
                
                
                
                
                
                
                
                
                
                <el-form-item label="费用">
                    <el-input v-model="form.Fee" />
                </el-form-item>
                
                
                
                <el-form-item label="井口数">
                    <el-input v-model="form.Number" />
                </el-form-item>
                
                
                
                <el-form-item label="备注">
                    <el-input v-model="form.Remark" />
                </el-form-item>
                
                
                
                <el-form-item label="钻机Id">
                    <el-input v-model="form.TypeId" />
                </el-form-item>
                
                
                
                <el-form-item label="井台id">
                    <el-input v-model="form.WellbayId" />
                </el-form-item>
                
                
                
                <el-form-item label="">
                    <el-input v-model="form.WellId" />
                </el-form-item>
                
                
            </el-form>
            <div class="flex justify-end margin-top-xs">
                <el-button plain @click="()=>{ this.showDialog = false }">取消</el-button>
                <el-button type="primary" :loading="saveLoading" @click="beforeSave">保存</el-button>
            </div>
        </myDialog>
    </div>
</template>

<script>
import MyDialog from '@/components/system/MyDialog'
import dialogMixin from '@/mixins/dialogMixin'
import { add, update, getById } from '@/api/estimationPredrilling'
export default {
  components: {
    MyDialog
  },
  mixins: [dialogMixin],
  props: {
    types: {
      type: Array
    }
  },
  data() {
    return {
      options: [
        {
          label: '是',
          value: true
        },
        {
          label: '否',
          value: false
        }
      ],
      saveLoading: false,
      form: {
          
              
      
          
              
            CombinedPrice: '',
          
          
              
         
          
              
              
          
              
         
          
              
            Fee: '',
          
          
              
            Number: '',
          
          
              
            Remark: '',
          
          
              
            TypeId: '',
          
          
              
            WellbayId: '',
          
          
              
            WellId: '',
          
          
      }
    }
  },
  created() {
    this.init()
  },
  methods: {
    init() {
      if (this.initParams.type === 1) {
        getById({ id: this.initParams.id }).then(res => {
          this.form = res.data
        })
      }
    },
    findTypeList(type) {
      var arr = this.types.filter(item => item.type === type)
      if (arr != null && arr.length > 0) {
        return arr[0].typeList
      }
      return []
    },
    handleReset() {

    },
    beforeSave() {
      this.saveLoading = true
      if (this.initParams.type === 0) {
        add(this.form).then(res => {
          this.saveLoading = false
          this.showDialog = false
          this.initParams.cb()
        })
      } else {
        update(this.form).then(res => {
          this.saveLoading = false
          this.showDialog = false
          this.initParams.cb()
        })
      }
    }

  }
}
</script>

<style lang="scss" scoped>
</style>

