import sys, os
from PySide6.QtWidgets import QApplication, QWidget
from PySide6 import QtCore

from plc import Ui_Form  # 引用UI文件，根据调用类型选择继承窗口类型：QMainWindow,QWidget,QDialog

class MyMainWindow(QWidget, Ui_Form):  # Widget界面时>>（QWidget,UI_。。），MainWindow类似
    def __init__(self, parent=None):
        super(MyMainWindow, self).__init__(parent)
        self.setupUi(self)

        # 按钮信号连接
        self.pushButton_27.clicked.connect(self.button)
        self.pushButton_28.clicked.connect(self.button_2)

        # 创建Qsettings对象
        # 方式1
        self.app_data = QSettings('Mysoft', '练习')
        # 方式2
        #self.app_data = QSettings('config.ini', QSettings.IniFormat)
        #self.app_data.setIniCodec('UTF-8')  # 设置ini文件编码为 UTF-8
        # 检查是否有数据进行初始化
        #if os.path.exists('./config.ini'):  # 方式2
        if self.app_data.value('time'):    #方式1
            # 如果存在数据就进行初始化
            self.init_info()
        else:
            # 没有数据就认为是第一次打开软件，进行第一次QSettings 数据存储
            self.save_info()

    def save_info(self):
        time = QDateTime.currentDateTime()  # 获取当前时间，并存储在self.qpp_data
        self.app_data.setValue('time', time.toString())  # 数据0：time.toString()为字符串类型
        self.text = self.lineEdit_8.text()  # 获取当前文本框的内容
        self.app_data.setValue('self.text1', self.text)  # 数据1：也是字符串类型
        a = 1  # 数据3：数值类型
       # list = [1, 'a', 2]  # 数据4：列表类型
        bool = self.checkBox_18.isChecked()
        print("bool",bool)# 数据:5：布尔类型
        #dict = {'a': 'abc', 'b': 2}  # 数据6：字典类型
        self.app_data.setValue('a', a)
        #self.app_data.setValue('list', list)
        self.app_data.setValue('bool', bool)
        #self.app_data.setValue('dict', dict)

    def init_info(self):
        time = self.app_data.value('time')
        self.text = self.app_data.value('self.text1')
        a = self.app_data.value('a')
        #list = self.app_data.value('list')
        bool = self.app_data.value('bool')
        #dict = self.app_data.value('dict')
        #print(time)  # 输出数据的值
        #print(type(time))  # 输出数据类型
        #print(self.text)
        #print(type(self.text))
        #print(a)
        #print(type(a))
       #print(list)
        #print(type(list))
        #print(bool)
        #print(type(bool))
        #print(dict)
        #print(type(dict))

        # 初始化文本框的内容
        self.lineEdit_8.setText(self.text)

    def button(self):
        # 进行数据保存
        self.save_info()

    def button_2(self):
        # 删除QSettings数据
        QSettings.clear(self.app_data)


if __name__ == "__main__":
    # 适配2k高分辨率屏幕
    QtCore.QCoreApplication.setAttribute(QtCore.Qt.AA_EnableHighDpiScaling)
    app = QApplication(sys.argv)
    myWin = MyMainWindow()
    myWin.show()
    sys.exit(app.exec_())




