from PySide6.QtWidgets import QMainWindow, QApplication
from aaa_main import Ui_MainWindow
from aaa_child import Ui_ChildWindow
import sys


class Main(QMainWindow,Ui_MainWindow):
    def __init__(self):
        super(Main, self).__init__()
        self.setupUi(self)

class Child(QMainWindow,Ui_ChildWindow):
    def __init__(self):
        super(Child, self).__init__()
        self.setupUi(self)
        self.pushButton.clicked.connect(self.close)
    def Open(self):
        self.show()

if __name__ =="__main__":
    app = QApplication(sys.argv)
    main = Main()
    child = Child()
    main.show()
    main.pushButton.clicked.connect(child.Open)
    main.pushButton.text()
    sys.exit(app.exec_())



