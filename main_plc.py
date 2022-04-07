from PySide6.QtWidgets import QMainWindow, QApplication,QWidget
from mainwindow import Ui_MainWindow
from plc import Ui_Form
import sys,os
from PySide6.QtCore import QSettings, QDateTime


class Main(QMainWindow,Ui_MainWindow):
    def __init__(self):
        super(Main, self).__init__()
        self.setupUi(self)

class Child(QWidget,Ui_Form):
    def __init__(self):
        super(Child, self).__init__()
        self.setupUi(self)
        self.pushButton_8.clicked.connect(self.save)
        self.pushButton_11.clicked.connect(self.cancel)
        self.pushButton.clicked.connect(self.close)
        self.pushButton_12.clicked.connect(self.save)
        self.pushButton_13.clicked.connect(self.cancel)
        self.pushButton_2.clicked.connect(self.close)
        self.pushButton_14.clicked.connect(self.save)
        self.pushButton_15.clicked.connect(self.cancel)
        self.pushButton_3.clicked.connect(self.close)
        self.pushButton_16.clicked.connect(self.save)
        self.pushButton_17.clicked.connect(self.cancel)
        self.pushButton_4.clicked.connect(self.close)
        self.pushButton_26.clicked.connect(self.save)
        self.pushButton_25.clicked.connect(self.cancel)
        self.pushButton_5.clicked.connect(self.close)
        self.pushButton_27.clicked.connect(self.save)
        self.pushButton_28.clicked.connect(self.cancel)
        self.pushButton_6.clicked.connect(self.close)
        self.pushButton_7.pressed.connect(self.longclick)

        # 创建Qsettings对象
        # 方式1
        self.app_data = QSettings('Mysoft', 'main_plc')


        # 检查是否有数据进行初始化
        if self.app_data.value('time'):    #方式1
            # 如果存在数据就进行初始化
            self.init_info()
        else:
            # 没有数据就认为是第一次打开软件，进行第一次QSettings 数据存储
            self.save_info()


    def save_info(self):
        time = QDateTime.currentDateTime()  # 获取当前时间，并存储在self.qpp_data
        self.app_data.setValue('time', time.toString())  # 数据0：time.toString()为字符串类型


        bool = self.checkBox_18.isChecked()
        a=self.spinBox.value()
        a2=self.spinBox_2.value()
        self.app_data.setValue('a', a)
        self.app_data.setValue('a2', a2)
        self.app_data.setValue('bool', bool)


    def init_info(self):
        time = self.app_data.value('time')

        a = self.app_data.value('a')
        a2=self.app_data.value('a2')
        bool = self.app_data.value('bool')

        print(time)  # 输出数据的值
        print(type(time))  # 输出数据类型

        print(a)
        print(type(a))

        print(bool)
        print(type(bool))



        self.checkBox_18.setChecked(bool=='true')
        self.spinBox.setValue(a)
        self.spinBox_2.setValue(a2)

    def save(self):
        # 进行数据保存
        self.save_info()
    def cancel(self):
        # 进行数据初始化
        self.init_info()
    def longclick(self):
        a=self.spinBox_3.value()
        b=self.spinBox_7.value()
        a=a+b
        self.spinBox_3.setValue(a)

    def Open(self):
        self.show()

if __name__ =="__main__":
    app = QApplication(sys.argv)
    main = Main()
    child = Child()
    main.show()
    main.pushButton.clicked.connect(child.Open)
    sys.exit(app.exec_())


