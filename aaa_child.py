# -*- coding: utf-8 -*-

################################################################################
## Form generated from reading UI file 'aaa_child.ui'
##
## Created by: Qt User Interface Compiler version 6.2.3
##
## WARNING! All changes made in this file will be lost when recompiling UI file!
################################################################################

from PySide6.QtCore import (QCoreApplication, QDate, QDateTime, QLocale,
    QMetaObject, QObject, QPoint, QRect,
    QSize, QTime, QUrl, Qt)
from PySide6.QtGui import (QBrush, QColor, QConicalGradient, QCursor,
    QFont, QFontDatabase, QGradient, QIcon,
    QImage, QKeySequence, QLinearGradient, QPainter,
    QPalette, QPixmap, QRadialGradient, QTransform)
from PySide6.QtWidgets import (QApplication, QMainWindow, QMenuBar, QPushButton,
    QSizePolicy, QStatusBar, QWidget)
from PySide6 import QtWidgets
import sys

class Ui_ChildWindow(object):
    def setupUi(self, ChildWindow):
        if not ChildWindow.objectName():
            ChildWindow.setObjectName(u"ChildWindow")
        ChildWindow.resize(800, 600)
        self.centralwidget = QWidget(ChildWindow)
        self.centralwidget.setObjectName(u"centralwidget")
        self.pushButton = QPushButton(self.centralwidget)
        self.pushButton.setObjectName(u"pushButton")
        self.pushButton.setGeometry(QRect(30, 140, 75, 24))
        ChildWindow.setCentralWidget(self.centralwidget)
        self.menubar = QMenuBar(ChildWindow)
        self.menubar.setObjectName(u"menubar")
        self.menubar.setGeometry(QRect(0, 0, 800, 22))
        ChildWindow.setMenuBar(self.menubar)
        self.statusbar = QStatusBar(ChildWindow)
        self.statusbar.setObjectName(u"statusbar")
        ChildWindow.setStatusBar(self.statusbar)

        self.retranslateUi(ChildWindow)

        QMetaObject.connectSlotsByName(ChildWindow)
    # setupUi

    def retranslateUi(self, ChildWindow):
        ChildWindow.setWindowTitle(QCoreApplication.translate("ChildWindow", u"ChildWindow", None))
        self.pushButton.setText(QCoreApplication.translate("ChildWindow", u"\u5173\u95ed", None))
    # retranslateUi
if __name__== "__main__":
    app = QApplication(sys.argv)
    ChildWindow = QMainWindow()  # QMainWindow装载需要的组件
    ui = Ui_ChildWindow()
    ui.setupUi(ChildWindow)  # 执行类中的setupUi方法
    ChildWindow.show()
    # 创建一个QApplication，即将开发的软件app
        #QChildWindow装载需要的组件

                                   #执行类中的setupUi方法

    sys.exit(app.exec_())
