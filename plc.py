# -*- coding: utf-8 -*-

################################################################################
## Form generated from reading UI file 'plc.ui'
##
## Created by: Qt User Interface Compiler version 6.2.3
##
## WARNING! All changes made in this file will be lost when recompiling UI file!
################################################################################

from PySide6.QtCore import (QCoreApplication, QDate, QDateTime, QLocale,
    QMetaObject, QObject, QPoint, QRect,QTimer,QSize, QTime, QUrl, Qt)
from PySide6.QtGui import (QBrush, QColor, QConicalGradient, QCursor,
    QFont, QFontDatabase, QGradient, QIcon,
    QImage, QKeySequence, QLinearGradient, QPainter,
    QPalette, QPixmap, QRadialGradient, QTransform)
from PySide6.QtWidgets import (QApplication, QCheckBox, QHBoxLayout, QLabel,
    QLineEdit, QPushButton, QSizePolicy, QSpacerItem,
    QSpinBox, QTabWidget, QTimeEdit, QVBoxLayout,
    QWidget)

class Ui_Form(object):
    def setupUi(self, Form):
        if not Form.objectName():
            Form.setObjectName(u"Form")
        Form.resize(650, 477)
        self.timer=QTimer()
        self.verticalLayout_11 = QVBoxLayout(Form)
        self.verticalLayout_11.setObjectName(u"verticalLayout_11")
        self.tabWidget = QTabWidget(Form)
        self.tabWidget.setObjectName(u"tabWidget")
        self.tab = QWidget()
        self.tab.setObjectName(u"tab")
        self.verticalLayout_9 = QVBoxLayout(self.tab)
        self.verticalLayout_9.setObjectName(u"verticalLayout_9")
        self.horizontalLayout_46 = QHBoxLayout()
        self.horizontalLayout_46.setObjectName(u"horizontalLayout_46")
        self.horizontalLayout_46.setContentsMargins(-1, 21, -1, -1)
        self.verticalLayout_7 = QVBoxLayout()
        self.verticalLayout_7.setObjectName(u"verticalLayout_7")
        self.checkBox_18 = QCheckBox(self.tab)
        self.checkBox_18.setObjectName(u"checkBox_18")

        self.verticalLayout_7.addWidget(self.checkBox_18)

        self.checkBox_19 = QCheckBox(self.tab)
        self.checkBox_19.setObjectName(u"checkBox_19")

        self.verticalLayout_7.addWidget(self.checkBox_19)

        self.checkBox_20 = QCheckBox(self.tab)
        self.checkBox_20.setObjectName(u"checkBox_20")

        self.verticalLayout_7.addWidget(self.checkBox_20)

        self.checkBox_21 = QCheckBox(self.tab)
        self.checkBox_21.setObjectName(u"checkBox_21")

        self.verticalLayout_7.addWidget(self.checkBox_21)

        self.checkBox_22 = QCheckBox(self.tab)
        self.checkBox_22.setObjectName(u"checkBox_22")

        self.verticalLayout_7.addWidget(self.checkBox_22)


        self.horizontalLayout_46.addLayout(self.verticalLayout_7)

        self.verticalLayout_8 = QVBoxLayout()
        self.verticalLayout_8.setObjectName(u"verticalLayout_8")
        self.checkBox_23 = QCheckBox(self.tab)
        self.checkBox_23.setObjectName(u"checkBox_23")

        self.verticalLayout_8.addWidget(self.checkBox_23)

        self.checkBox_24 = QCheckBox(self.tab)
        self.checkBox_24.setObjectName(u"checkBox_24")

        self.verticalLayout_8.addWidget(self.checkBox_24)

        self.checkBox_25 = QCheckBox(self.tab)
        self.checkBox_25.setObjectName(u"checkBox_25")

        self.verticalLayout_8.addWidget(self.checkBox_25)

        self.checkBox_26 = QCheckBox(self.tab)
        self.checkBox_26.setObjectName(u"checkBox_26")

        self.verticalLayout_8.addWidget(self.checkBox_26)

        self.checkBox_30 = QCheckBox(self.tab)
        self.checkBox_30.setObjectName(u"checkBox_30")

        self.verticalLayout_8.addWidget(self.checkBox_30)


        self.horizontalLayout_46.addLayout(self.verticalLayout_8)


        self.verticalLayout_9.addLayout(self.horizontalLayout_46)

        self.horizontalLayout_35 = QHBoxLayout()
        self.horizontalLayout_35.setObjectName(u"horizontalLayout_35")
        self.horizontalLayout_2 = QHBoxLayout()
        self.horizontalLayout_2.setObjectName(u"horizontalLayout_2")
        self.label_10 = QLabel(self.tab)
        self.label_10.setObjectName(u"label_10")

        self.horizontalLayout_2.addWidget(self.label_10)

        self.spinBox_2 = QSpinBox(self.tab)
        self.spinBox_2.setObjectName(u"spinBox_2")
        sizePolicy = QSizePolicy(QSizePolicy.Fixed, QSizePolicy.Fixed)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.spinBox_2.sizePolicy().hasHeightForWidth())
        self.spinBox_2.setSizePolicy(sizePolicy)

        self.horizontalLayout_2.addWidget(self.spinBox_2)


        self.horizontalLayout_35.addLayout(self.horizontalLayout_2)

        self.horizontalLayout = QHBoxLayout()
        self.horizontalLayout.setObjectName(u"horizontalLayout")
        self.label_9 = QLabel(self.tab)
        self.label_9.setObjectName(u"label_9")

        self.horizontalLayout.addWidget(self.label_9)

        self.spinBox = QSpinBox(self.tab)
        self.spinBox.setObjectName(u"spinBox")
        sizePolicy.setHeightForWidth(self.spinBox.sizePolicy().hasHeightForWidth())
        self.spinBox.setSizePolicy(sizePolicy)

        self.horizontalLayout.addWidget(self.spinBox)

        self.horizontalSpacer_22 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout.addItem(self.horizontalSpacer_22)


        self.horizontalLayout_35.addLayout(self.horizontalLayout)


        self.verticalLayout_9.addLayout(self.horizontalLayout_35)

        self.horizontalLayout_37 = QHBoxLayout()
        self.horizontalLayout_37.setObjectName(u"horizontalLayout_37")
        self.horizontalSpacer_16 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_37.addItem(self.horizontalSpacer_16)

        self.pushButton_8 = QPushButton(self.tab)
        self.pushButton_8.setObjectName(u"pushButton_8")

        self.horizontalLayout_37.addWidget(self.pushButton_8)

        self.pushButton_11 = QPushButton(self.tab)
        self.pushButton_11.setObjectName(u"pushButton_11")

        self.horizontalLayout_37.addWidget(self.pushButton_11)

        self.pushButton = QPushButton(self.tab)
        self.pushButton.setObjectName(u"pushButton")

        self.horizontalLayout_37.addWidget(self.pushButton)


        self.verticalLayout_9.addLayout(self.horizontalLayout_37)

        self.tabWidget.addTab(self.tab, "")
        self.tab_2 = QWidget()
        self.tab_2.setObjectName(u"tab_2")
        self.verticalLayout_39 = QVBoxLayout(self.tab_2)
        self.verticalLayout_39.setObjectName(u"verticalLayout_39")
        self.horizontalLayout_8 = QHBoxLayout()
        self.horizontalLayout_8.setObjectName(u"horizontalLayout_8")
        self.horizontalLayout_8.setContentsMargins(-1, -1, 82, -1)
        self.label_12 = QLabel(self.tab_2)
        self.label_12.setObjectName(u"label_12")

        self.horizontalLayout_8.addWidget(self.label_12)

        self.lineEdit_17 = QLineEdit(self.tab_2)
        self.lineEdit_17.setObjectName(u"lineEdit_17")

        self.horizontalLayout_8.addWidget(self.lineEdit_17)

        self.horizontalSpacer = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_8.addItem(self.horizontalSpacer)

        self.pushButton_7 = QPushButton(self.tab_2)
        self.pushButton_7.setObjectName(u"pushButton_7")
        self.pushButton_7.setAutoRepeat(True)

        self.horizontalLayout_8.addWidget(self.pushButton_7)

        self.spinBox_3 = QSpinBox(self.tab_2)
        self.spinBox_3.setObjectName(u"spinBox_3")
        sizePolicy1 = QSizePolicy(QSizePolicy.Expanding, QSizePolicy.Fixed)
        sizePolicy1.setHorizontalStretch(0)
        sizePolicy1.setVerticalStretch(0)
        sizePolicy1.setHeightForWidth(self.spinBox_3.sizePolicy().hasHeightForWidth())
        self.spinBox_3.setSizePolicy(sizePolicy1)
        self.spinBox_3.setMaximum(1000)

        self.horizontalLayout_8.addWidget(self.spinBox_3)

        self.pushButton_33 = QPushButton(self.tab_2)
        self.pushButton_33.setObjectName(u"pushButton_33")

        self.horizontalLayout_8.addWidget(self.pushButton_33)


        self.verticalLayout_39.addLayout(self.horizontalLayout_8)

        self.horizontalLayout_79 = QHBoxLayout()
        self.horizontalLayout_79.setObjectName(u"horizontalLayout_79")
        self.horizontalLayout_79.setContentsMargins(-1, -1, 82, -1)
        self.label_11 = QLabel(self.tab_2)
        self.label_11.setObjectName(u"label_11")

        self.horizontalLayout_79.addWidget(self.label_11)

        self.lineEdit_19 = QLineEdit(self.tab_2)
        self.lineEdit_19.setObjectName(u"lineEdit_19")

        self.horizontalLayout_79.addWidget(self.lineEdit_19)

        self.horizontalSpacer_2 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_79.addItem(self.horizontalSpacer_2)

        self.pushButton_34 = QPushButton(self.tab_2)
        self.pushButton_34.setObjectName(u"pushButton_34")

        self.horizontalLayout_79.addWidget(self.pushButton_34)

        self.spinBox_4 = QSpinBox(self.tab_2)
        self.spinBox_4.setObjectName(u"spinBox_4")
        sizePolicy1.setHeightForWidth(self.spinBox_4.sizePolicy().hasHeightForWidth())
        self.spinBox_4.setSizePolicy(sizePolicy1)
        self.spinBox_4.setMaximum(1000)

        self.horizontalLayout_79.addWidget(self.spinBox_4)

        self.pushButton_35 = QPushButton(self.tab_2)
        self.pushButton_35.setObjectName(u"pushButton_35")

        self.horizontalLayout_79.addWidget(self.pushButton_35)


        self.verticalLayout_39.addLayout(self.horizontalLayout_79)

        self.horizontalLayout_80 = QHBoxLayout()
        self.horizontalLayout_80.setObjectName(u"horizontalLayout_80")
        self.horizontalLayout_80.setContentsMargins(-1, -1, 82, -1)
        self.label_13 = QLabel(self.tab_2)
        self.label_13.setObjectName(u"label_13")

        self.horizontalLayout_80.addWidget(self.label_13)

        self.lineEdit_21 = QLineEdit(self.tab_2)
        self.lineEdit_21.setObjectName(u"lineEdit_21")

        self.horizontalLayout_80.addWidget(self.lineEdit_21)

        self.horizontalSpacer_3 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_80.addItem(self.horizontalSpacer_3)

        self.pushButton_36 = QPushButton(self.tab_2)
        self.pushButton_36.setObjectName(u"pushButton_36")

        self.horizontalLayout_80.addWidget(self.pushButton_36)

        self.spinBox_5 = QSpinBox(self.tab_2)
        self.spinBox_5.setObjectName(u"spinBox_5")
        sizePolicy1.setHeightForWidth(self.spinBox_5.sizePolicy().hasHeightForWidth())
        self.spinBox_5.setSizePolicy(sizePolicy1)
        self.spinBox_5.setMaximum(1000)

        self.horizontalLayout_80.addWidget(self.spinBox_5)

        self.pushButton_37 = QPushButton(self.tab_2)
        self.pushButton_37.setObjectName(u"pushButton_37")

        self.horizontalLayout_80.addWidget(self.pushButton_37)


        self.verticalLayout_39.addLayout(self.horizontalLayout_80)

        self.horizontalLayout_81 = QHBoxLayout()
        self.horizontalLayout_81.setObjectName(u"horizontalLayout_81")
        self.horizontalLayout_81.setContentsMargins(-1, -1, 82, -1)
        self.label_14 = QLabel(self.tab_2)
        self.label_14.setObjectName(u"label_14")

        self.horizontalLayout_81.addWidget(self.label_14)

        self.lineEdit_23 = QLineEdit(self.tab_2)
        self.lineEdit_23.setObjectName(u"lineEdit_23")

        self.horizontalLayout_81.addWidget(self.lineEdit_23)

        self.horizontalSpacer_4 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_81.addItem(self.horizontalSpacer_4)

        self.pushButton_38 = QPushButton(self.tab_2)
        self.pushButton_38.setObjectName(u"pushButton_38")

        self.horizontalLayout_81.addWidget(self.pushButton_38)

        self.spinBox_6 = QSpinBox(self.tab_2)
        self.spinBox_6.setObjectName(u"spinBox_6")
        sizePolicy1.setHeightForWidth(self.spinBox_6.sizePolicy().hasHeightForWidth())
        self.spinBox_6.setSizePolicy(sizePolicy1)
        self.spinBox_6.setMaximum(1000)

        self.horizontalLayout_81.addWidget(self.spinBox_6)

        self.pushButton_39 = QPushButton(self.tab_2)
        self.pushButton_39.setObjectName(u"pushButton_39")

        self.horizontalLayout_81.addWidget(self.pushButton_39)


        self.verticalLayout_39.addLayout(self.horizontalLayout_81)

        self.horizontalLayout_82 = QHBoxLayout()
        self.horizontalLayout_82.setObjectName(u"horizontalLayout_82")
        self.horizontalLayout_82.setContentsMargins(-1, -1, 82, -1)
        self.label_15 = QLabel(self.tab_2)
        self.label_15.setObjectName(u"label_15")

        self.horizontalLayout_82.addWidget(self.label_15)

        self.lineEdit_25 = QLineEdit(self.tab_2)
        self.lineEdit_25.setObjectName(u"lineEdit_25")

        self.horizontalLayout_82.addWidget(self.lineEdit_25)

        self.horizontalSpacer_5 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_82.addItem(self.horizontalSpacer_5)

        self.pushButton_40 = QPushButton(self.tab_2)
        self.pushButton_40.setObjectName(u"pushButton_40")

        self.horizontalLayout_82.addWidget(self.pushButton_40)

        self.spinBox_9 = QSpinBox(self.tab_2)
        self.spinBox_9.setObjectName(u"spinBox_9")
        sizePolicy1.setHeightForWidth(self.spinBox_9.sizePolicy().hasHeightForWidth())
        self.spinBox_9.setSizePolicy(sizePolicy1)
        self.spinBox_9.setMaximum(1000)

        self.horizontalLayout_82.addWidget(self.spinBox_9)

        self.pushButton_41 = QPushButton(self.tab_2)
        self.pushButton_41.setObjectName(u"pushButton_41")

        self.horizontalLayout_82.addWidget(self.pushButton_41)


        self.verticalLayout_39.addLayout(self.horizontalLayout_82)

        self.horizontalLayout_83 = QHBoxLayout()
        self.horizontalLayout_83.setObjectName(u"horizontalLayout_83")
        self.horizontalLayout_83.setContentsMargins(-1, -1, 82, -1)
        self.label_16 = QLabel(self.tab_2)
        self.label_16.setObjectName(u"label_16")

        self.horizontalLayout_83.addWidget(self.label_16)

        self.lineEdit_27 = QLineEdit(self.tab_2)
        self.lineEdit_27.setObjectName(u"lineEdit_27")

        self.horizontalLayout_83.addWidget(self.lineEdit_27)

        self.horizontalSpacer_6 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_83.addItem(self.horizontalSpacer_6)

        self.pushButton_42 = QPushButton(self.tab_2)
        self.pushButton_42.setObjectName(u"pushButton_42")

        self.horizontalLayout_83.addWidget(self.pushButton_42)

        self.spinBox_10 = QSpinBox(self.tab_2)
        self.spinBox_10.setObjectName(u"spinBox_10")
        sizePolicy1.setHeightForWidth(self.spinBox_10.sizePolicy().hasHeightForWidth())
        self.spinBox_10.setSizePolicy(sizePolicy1)
        self.spinBox_10.setMaximum(1000)

        self.horizontalLayout_83.addWidget(self.spinBox_10)

        self.pushButton_43 = QPushButton(self.tab_2)
        self.pushButton_43.setObjectName(u"pushButton_43")

        self.horizontalLayout_83.addWidget(self.pushButton_43)


        self.verticalLayout_39.addLayout(self.horizontalLayout_83)

        self.horizontalLayout_84 = QHBoxLayout()
        self.horizontalLayout_84.setObjectName(u"horizontalLayout_84")
        self.horizontalLayout_84.setContentsMargins(-1, -1, 82, -1)
        self.label_18 = QLabel(self.tab_2)
        self.label_18.setObjectName(u"label_18")

        self.horizontalLayout_84.addWidget(self.label_18)

        self.lineEdit_18 = QLineEdit(self.tab_2)
        self.lineEdit_18.setObjectName(u"lineEdit_18")

        self.horizontalLayout_84.addWidget(self.lineEdit_18)

        self.horizontalSpacer_7 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_84.addItem(self.horizontalSpacer_7)

        self.pushButton_44 = QPushButton(self.tab_2)
        self.pushButton_44.setObjectName(u"pushButton_44")

        self.horizontalLayout_84.addWidget(self.pushButton_44)

        self.spinBox_11 = QSpinBox(self.tab_2)
        self.spinBox_11.setObjectName(u"spinBox_11")
        sizePolicy1.setHeightForWidth(self.spinBox_11.sizePolicy().hasHeightForWidth())
        self.spinBox_11.setSizePolicy(sizePolicy1)
        self.spinBox_11.setMaximum(1000)

        self.horizontalLayout_84.addWidget(self.spinBox_11)

        self.pushButton_45 = QPushButton(self.tab_2)
        self.pushButton_45.setObjectName(u"pushButton_45")

        self.horizontalLayout_84.addWidget(self.pushButton_45)


        self.verticalLayout_39.addLayout(self.horizontalLayout_84)

        self.horizontalLayout_85 = QHBoxLayout()
        self.horizontalLayout_85.setObjectName(u"horizontalLayout_85")
        self.horizontalLayout_85.setContentsMargins(-1, -1, 82, -1)
        self.label_17 = QLabel(self.tab_2)
        self.label_17.setObjectName(u"label_17")

        self.horizontalLayout_85.addWidget(self.label_17)

        self.lineEdit_20 = QLineEdit(self.tab_2)
        self.lineEdit_20.setObjectName(u"lineEdit_20")

        self.horizontalLayout_85.addWidget(self.lineEdit_20)

        self.horizontalSpacer_8 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_85.addItem(self.horizontalSpacer_8)

        self.pushButton_46 = QPushButton(self.tab_2)
        self.pushButton_46.setObjectName(u"pushButton_46")

        self.horizontalLayout_85.addWidget(self.pushButton_46)

        self.spinBox_12 = QSpinBox(self.tab_2)
        self.spinBox_12.setObjectName(u"spinBox_12")
        sizePolicy1.setHeightForWidth(self.spinBox_12.sizePolicy().hasHeightForWidth())
        self.spinBox_12.setSizePolicy(sizePolicy1)
        self.spinBox_12.setMaximum(1000)

        self.horizontalLayout_85.addWidget(self.spinBox_12)

        self.pushButton_47 = QPushButton(self.tab_2)
        self.pushButton_47.setObjectName(u"pushButton_47")

        self.horizontalLayout_85.addWidget(self.pushButton_47)


        self.verticalLayout_39.addLayout(self.horizontalLayout_85)

        self.horizontalLayout_36 = QHBoxLayout()
        self.horizontalLayout_36.setObjectName(u"horizontalLayout_36")
        self.label = QLabel(self.tab_2)
        self.label.setObjectName(u"label")

        self.horizontalLayout_36.addWidget(self.label)

        self.spinBox_7 = QSpinBox(self.tab_2)
        self.spinBox_7.setObjectName(u"spinBox_7")

        self.horizontalLayout_36.addWidget(self.spinBox_7)

        self.spacer15 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_36.addItem(self.spacer15)

        self.pushButton_12 = QPushButton(self.tab_2)
        self.pushButton_12.setObjectName(u"pushButton_12")

        self.horizontalLayout_36.addWidget(self.pushButton_12)

        self.pushButton_13 = QPushButton(self.tab_2)
        self.pushButton_13.setObjectName(u"pushButton_13")

        self.horizontalLayout_36.addWidget(self.pushButton_13)

        self.pushButton_2 = QPushButton(self.tab_2)
        self.pushButton_2.setObjectName(u"pushButton_2")

        self.horizontalLayout_36.addWidget(self.pushButton_2)


        self.verticalLayout_39.addLayout(self.horizontalLayout_36)

        self.tabWidget.addTab(self.tab_2, "")
        self.tab_3 = QWidget()
        self.tab_3.setObjectName(u"tab_3")
        self.verticalLayout_38 = QVBoxLayout(self.tab_3)
        self.verticalLayout_38.setObjectName(u"verticalLayout_38")
        self.horizontalLayout_86 = QHBoxLayout()
        self.horizontalLayout_86.setObjectName(u"horizontalLayout_86")
        self.horizontalLayout_86.setContentsMargins(-1, -1, 82, -1)
        self.label_4 = QLabel(self.tab_3)
        self.label_4.setObjectName(u"label_4")

        self.horizontalLayout_86.addWidget(self.label_4)

        self.lineEdit_32 = QLineEdit(self.tab_3)
        self.lineEdit_32.setObjectName(u"lineEdit_32")

        self.horizontalLayout_86.addWidget(self.lineEdit_32)

        self.horizontalSpacer_9 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_86.addItem(self.horizontalSpacer_9)

        self.pushButton_48 = QPushButton(self.tab_3)
        self.pushButton_48.setObjectName(u"pushButton_48")
        sizePolicy.setHeightForWidth(self.pushButton_48.sizePolicy().hasHeightForWidth())
        self.pushButton_48.setSizePolicy(sizePolicy)
        self.pushButton_48.setAutoRepeat(False)

        self.horizontalLayout_86.addWidget(self.pushButton_48)

        self.spinBox_13 = QSpinBox(self.tab_3)
        self.spinBox_13.setObjectName(u"spinBox_13")
        sizePolicy1.setHeightForWidth(self.spinBox_13.sizePolicy().hasHeightForWidth())
        self.spinBox_13.setSizePolicy(sizePolicy1)
        self.spinBox_13.setMaximum(1000)

        self.horizontalLayout_86.addWidget(self.spinBox_13)

        self.pushButton_49 = QPushButton(self.tab_3)
        self.pushButton_49.setObjectName(u"pushButton_49")
        sizePolicy.setHeightForWidth(self.pushButton_49.sizePolicy().hasHeightForWidth())
        self.pushButton_49.setSizePolicy(sizePolicy)

        self.horizontalLayout_86.addWidget(self.pushButton_49)


        self.verticalLayout_38.addLayout(self.horizontalLayout_86)

        self.horizontalLayout_87 = QHBoxLayout()
        self.horizontalLayout_87.setObjectName(u"horizontalLayout_87")
        self.horizontalLayout_87.setContentsMargins(-1, -1, 82, -1)
        self.label_8 = QLabel(self.tab_3)
        self.label_8.setObjectName(u"label_8")

        self.horizontalLayout_87.addWidget(self.label_8)

        self.lineEdit_66 = QLineEdit(self.tab_3)
        self.lineEdit_66.setObjectName(u"lineEdit_66")

        self.horizontalLayout_87.addWidget(self.lineEdit_66)

        self.horizontalSpacer_10 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_87.addItem(self.horizontalSpacer_10)

        self.pushButton_50 = QPushButton(self.tab_3)
        self.pushButton_50.setObjectName(u"pushButton_50")

        self.horizontalLayout_87.addWidget(self.pushButton_50)

        self.spinBox_14 = QSpinBox(self.tab_3)
        self.spinBox_14.setObjectName(u"spinBox_14")
        sizePolicy1.setHeightForWidth(self.spinBox_14.sizePolicy().hasHeightForWidth())
        self.spinBox_14.setSizePolicy(sizePolicy1)
        self.spinBox_14.setMaximum(1000)

        self.horizontalLayout_87.addWidget(self.spinBox_14)

        self.pushButton_51 = QPushButton(self.tab_3)
        self.pushButton_51.setObjectName(u"pushButton_51")

        self.horizontalLayout_87.addWidget(self.pushButton_51)


        self.verticalLayout_38.addLayout(self.horizontalLayout_87)

        self.horizontalLayout_88 = QHBoxLayout()
        self.horizontalLayout_88.setObjectName(u"horizontalLayout_88")
        self.horizontalLayout_88.setContentsMargins(-1, -1, 82, -1)
        self.label_85 = QLabel(self.tab_3)
        self.label_85.setObjectName(u"label_85")

        self.horizontalLayout_88.addWidget(self.label_85)

        self.lineEdit_63 = QLineEdit(self.tab_3)
        self.lineEdit_63.setObjectName(u"lineEdit_63")

        self.horizontalLayout_88.addWidget(self.lineEdit_63)

        self.horizontalSpacer_11 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_88.addItem(self.horizontalSpacer_11)

        self.pushButton_52 = QPushButton(self.tab_3)
        self.pushButton_52.setObjectName(u"pushButton_52")

        self.horizontalLayout_88.addWidget(self.pushButton_52)

        self.spinBox_15 = QSpinBox(self.tab_3)
        self.spinBox_15.setObjectName(u"spinBox_15")
        sizePolicy1.setHeightForWidth(self.spinBox_15.sizePolicy().hasHeightForWidth())
        self.spinBox_15.setSizePolicy(sizePolicy1)
        self.spinBox_15.setMaximum(1000)

        self.horizontalLayout_88.addWidget(self.spinBox_15)

        self.pushButton_53 = QPushButton(self.tab_3)
        self.pushButton_53.setObjectName(u"pushButton_53")

        self.horizontalLayout_88.addWidget(self.pushButton_53)


        self.verticalLayout_38.addLayout(self.horizontalLayout_88)

        self.horizontalLayout_89 = QHBoxLayout()
        self.horizontalLayout_89.setObjectName(u"horizontalLayout_89")
        self.horizontalLayout_89.setContentsMargins(-1, -1, 82, -1)
        self.label_86 = QLabel(self.tab_3)
        self.label_86.setObjectName(u"label_86")

        self.horizontalLayout_89.addWidget(self.label_86)

        self.lineEdit_65 = QLineEdit(self.tab_3)
        self.lineEdit_65.setObjectName(u"lineEdit_65")

        self.horizontalLayout_89.addWidget(self.lineEdit_65)

        self.horizontalSpacer_12 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_89.addItem(self.horizontalSpacer_12)

        self.pushButton_54 = QPushButton(self.tab_3)
        self.pushButton_54.setObjectName(u"pushButton_54")

        self.horizontalLayout_89.addWidget(self.pushButton_54)

        self.spinBox_16 = QSpinBox(self.tab_3)
        self.spinBox_16.setObjectName(u"spinBox_16")
        sizePolicy1.setHeightForWidth(self.spinBox_16.sizePolicy().hasHeightForWidth())
        self.spinBox_16.setSizePolicy(sizePolicy1)
        self.spinBox_16.setMaximum(1000)

        self.horizontalLayout_89.addWidget(self.spinBox_16)

        self.pushButton_55 = QPushButton(self.tab_3)
        self.pushButton_55.setObjectName(u"pushButton_55")

        self.horizontalLayout_89.addWidget(self.pushButton_55)


        self.verticalLayout_38.addLayout(self.horizontalLayout_89)

        self.horizontalLayout_90 = QHBoxLayout()
        self.horizontalLayout_90.setObjectName(u"horizontalLayout_90")
        self.horizontalLayout_90.setContentsMargins(-1, -1, 82, -1)
        self.label_87 = QLabel(self.tab_3)
        self.label_87.setObjectName(u"label_87")

        self.horizontalLayout_90.addWidget(self.label_87)

        self.lineEdit_67 = QLineEdit(self.tab_3)
        self.lineEdit_67.setObjectName(u"lineEdit_67")

        self.horizontalLayout_90.addWidget(self.lineEdit_67)

        self.horizontalSpacer_13 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_90.addItem(self.horizontalSpacer_13)

        self.pushButton_56 = QPushButton(self.tab_3)
        self.pushButton_56.setObjectName(u"pushButton_56")

        self.horizontalLayout_90.addWidget(self.pushButton_56)

        self.spinBox_17 = QSpinBox(self.tab_3)
        self.spinBox_17.setObjectName(u"spinBox_17")
        sizePolicy1.setHeightForWidth(self.spinBox_17.sizePolicy().hasHeightForWidth())
        self.spinBox_17.setSizePolicy(sizePolicy1)
        self.spinBox_17.setMaximum(1000)

        self.horizontalLayout_90.addWidget(self.spinBox_17)

        self.pushButton_57 = QPushButton(self.tab_3)
        self.pushButton_57.setObjectName(u"pushButton_57")

        self.horizontalLayout_90.addWidget(self.pushButton_57)


        self.verticalLayout_38.addLayout(self.horizontalLayout_90)

        self.horizontalLayout_91 = QHBoxLayout()
        self.horizontalLayout_91.setObjectName(u"horizontalLayout_91")
        self.horizontalLayout_91.setContentsMargins(-1, -1, 82, -1)
        self.label_89 = QLabel(self.tab_3)
        self.label_89.setObjectName(u"label_89")

        self.horizontalLayout_91.addWidget(self.label_89)

        self.lineEdit_64 = QLineEdit(self.tab_3)
        self.lineEdit_64.setObjectName(u"lineEdit_64")

        self.horizontalLayout_91.addWidget(self.lineEdit_64)

        self.horizontalSpacer_14 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_91.addItem(self.horizontalSpacer_14)

        self.pushButton_58 = QPushButton(self.tab_3)
        self.pushButton_58.setObjectName(u"pushButton_58")

        self.horizontalLayout_91.addWidget(self.pushButton_58)

        self.spinBox_18 = QSpinBox(self.tab_3)
        self.spinBox_18.setObjectName(u"spinBox_18")
        sizePolicy1.setHeightForWidth(self.spinBox_18.sizePolicy().hasHeightForWidth())
        self.spinBox_18.setSizePolicy(sizePolicy1)
        self.spinBox_18.setMaximum(1000)

        self.horizontalLayout_91.addWidget(self.spinBox_18)

        self.pushButton_59 = QPushButton(self.tab_3)
        self.pushButton_59.setObjectName(u"pushButton_59")

        self.horizontalLayout_91.addWidget(self.pushButton_59)


        self.verticalLayout_38.addLayout(self.horizontalLayout_91)

        self.horizontalLayout_92 = QHBoxLayout()
        self.horizontalLayout_92.setObjectName(u"horizontalLayout_92")
        self.horizontalLayout_92.setContentsMargins(-1, -1, 82, -1)
        self.label_88 = QLabel(self.tab_3)
        self.label_88.setObjectName(u"label_88")

        self.horizontalLayout_92.addWidget(self.label_88)

        self.lineEdit_68 = QLineEdit(self.tab_3)
        self.lineEdit_68.setObjectName(u"lineEdit_68")

        self.horizontalLayout_92.addWidget(self.lineEdit_68)

        self.horizontalSpacer_15 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_92.addItem(self.horizontalSpacer_15)

        self.pushButton_60 = QPushButton(self.tab_3)
        self.pushButton_60.setObjectName(u"pushButton_60")

        self.horizontalLayout_92.addWidget(self.pushButton_60)

        self.spinBox_20 = QSpinBox(self.tab_3)
        self.spinBox_20.setObjectName(u"spinBox_20")
        sizePolicy1.setHeightForWidth(self.spinBox_20.sizePolicy().hasHeightForWidth())
        self.spinBox_20.setSizePolicy(sizePolicy1)
        self.spinBox_20.setMaximum(1000)

        self.horizontalLayout_92.addWidget(self.spinBox_20)

        self.pushButton_61 = QPushButton(self.tab_3)
        self.pushButton_61.setObjectName(u"pushButton_61")

        self.horizontalLayout_92.addWidget(self.pushButton_61)


        self.verticalLayout_38.addLayout(self.horizontalLayout_92)

        self.horizontalLayout_7 = QHBoxLayout()
        self.horizontalLayout_7.setObjectName(u"horizontalLayout_7")
        self.horizontalLayout_7.setContentsMargins(-1, -1, 7, -1)
        self.label_2 = QLabel(self.tab_3)
        self.label_2.setObjectName(u"label_2")

        self.horizontalLayout_7.addWidget(self.label_2)

        self.spinBox_8 = QSpinBox(self.tab_3)
        self.spinBox_8.setObjectName(u"spinBox_8")

        self.horizontalLayout_7.addWidget(self.spinBox_8)

        self.horizontalSpacer_17 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_7.addItem(self.horizontalSpacer_17)

        self.pushButton_14 = QPushButton(self.tab_3)
        self.pushButton_14.setObjectName(u"pushButton_14")

        self.horizontalLayout_7.addWidget(self.pushButton_14)

        self.pushButton_15 = QPushButton(self.tab_3)
        self.pushButton_15.setObjectName(u"pushButton_15")

        self.horizontalLayout_7.addWidget(self.pushButton_15)

        self.pushButton_3 = QPushButton(self.tab_3)
        self.pushButton_3.setObjectName(u"pushButton_3")

        self.horizontalLayout_7.addWidget(self.pushButton_3)


        self.verticalLayout_38.addLayout(self.horizontalLayout_7)

        self.tabWidget.addTab(self.tab_3, "")
        self.tab_4 = QWidget()
        self.tab_4.setObjectName(u"tab_4")
        self.verticalLayout_6 = QVBoxLayout(self.tab_4)
        self.verticalLayout_6.setObjectName(u"verticalLayout_6")
        self.horizontalLayout_34 = QHBoxLayout()
        self.horizontalLayout_34.setObjectName(u"horizontalLayout_34")
        self.horizontalLayout_33 = QHBoxLayout()
        self.horizontalLayout_33.setObjectName(u"horizontalLayout_33")
        self.verticalLayout = QVBoxLayout()
        self.verticalLayout.setObjectName(u"verticalLayout")
        self.checkBox_6 = QCheckBox(self.tab_4)
        self.checkBox_6.setObjectName(u"checkBox_6")

        self.verticalLayout.addWidget(self.checkBox_6)

        self.checkBox_7 = QCheckBox(self.tab_4)
        self.checkBox_7.setObjectName(u"checkBox_7")

        self.verticalLayout.addWidget(self.checkBox_7)

        self.checkBox_8 = QCheckBox(self.tab_4)
        self.checkBox_8.setObjectName(u"checkBox_8")

        self.verticalLayout.addWidget(self.checkBox_8)

        self.checkBox_9 = QCheckBox(self.tab_4)
        self.checkBox_9.setObjectName(u"checkBox_9")

        self.verticalLayout.addWidget(self.checkBox_9)

        self.checkBox_10 = QCheckBox(self.tab_4)
        self.checkBox_10.setObjectName(u"checkBox_10")

        self.verticalLayout.addWidget(self.checkBox_10)

        self.checkBox_11 = QCheckBox(self.tab_4)
        self.checkBox_11.setObjectName(u"checkBox_11")

        self.verticalLayout.addWidget(self.checkBox_11)

        self.checkBox_12 = QCheckBox(self.tab_4)
        self.checkBox_12.setObjectName(u"checkBox_12")

        self.verticalLayout.addWidget(self.checkBox_12)

        self.checkBox_13 = QCheckBox(self.tab_4)
        self.checkBox_13.setObjectName(u"checkBox_13")

        self.verticalLayout.addWidget(self.checkBox_13)


        self.horizontalLayout_33.addLayout(self.verticalLayout)

        self.verticalLayout_2 = QVBoxLayout()
        self.verticalLayout_2.setObjectName(u"verticalLayout_2")
        self.checkBox_28 = QCheckBox(self.tab_4)
        self.checkBox_28.setObjectName(u"checkBox_28")

        self.verticalLayout_2.addWidget(self.checkBox_28)

        self.checkBox_29 = QCheckBox(self.tab_4)
        self.checkBox_29.setObjectName(u"checkBox_29")

        self.verticalLayout_2.addWidget(self.checkBox_29)

        self.checkBox_14 = QCheckBox(self.tab_4)
        self.checkBox_14.setObjectName(u"checkBox_14")
        self.checkBox_14.setChecked(True)

        self.verticalLayout_2.addWidget(self.checkBox_14)

        self.checkBox_15 = QCheckBox(self.tab_4)
        self.checkBox_15.setObjectName(u"checkBox_15")

        self.verticalLayout_2.addWidget(self.checkBox_15)

        self.checkBox_16 = QCheckBox(self.tab_4)
        self.checkBox_16.setObjectName(u"checkBox_16")

        self.verticalLayout_2.addWidget(self.checkBox_16)

        self.checkBox_17 = QCheckBox(self.tab_4)
        self.checkBox_17.setObjectName(u"checkBox_17")

        self.verticalLayout_2.addWidget(self.checkBox_17)

        self.checkBox_27 = QCheckBox(self.tab_4)
        self.checkBox_27.setObjectName(u"checkBox_27")

        self.verticalLayout_2.addWidget(self.checkBox_27)


        self.horizontalLayout_33.addLayout(self.verticalLayout_2)


        self.horizontalLayout_34.addLayout(self.horizontalLayout_33)

        self.verticalLayout_3 = QVBoxLayout()
        self.verticalLayout_3.setObjectName(u"verticalLayout_3")
        self.verticalLayout_23 = QVBoxLayout()
        self.verticalLayout_23.setObjectName(u"verticalLayout_23")
        self.pushButton_22 = QPushButton(self.tab_4)
        self.pushButton_22.setObjectName(u"pushButton_22")
        sizePolicy.setHeightForWidth(self.pushButton_22.sizePolicy().hasHeightForWidth())
        self.pushButton_22.setSizePolicy(sizePolicy)

        self.verticalLayout_23.addWidget(self.pushButton_22)

        self.pushButton_23 = QPushButton(self.tab_4)
        self.pushButton_23.setObjectName(u"pushButton_23")
        sizePolicy.setHeightForWidth(self.pushButton_23.sizePolicy().hasHeightForWidth())
        self.pushButton_23.setSizePolicy(sizePolicy)

        self.verticalLayout_23.addWidget(self.pushButton_23)

        self.pushButton_24 = QPushButton(self.tab_4)
        self.pushButton_24.setObjectName(u"pushButton_24")
        sizePolicy.setHeightForWidth(self.pushButton_24.sizePolicy().hasHeightForWidth())
        self.pushButton_24.setSizePolicy(sizePolicy)

        self.verticalLayout_23.addWidget(self.pushButton_24)


        self.verticalLayout_3.addLayout(self.verticalLayout_23)

        self.verticalLayout_24 = QVBoxLayout()
        self.verticalLayout_24.setObjectName(u"verticalLayout_24")
        self.verticalLayout_24.setContentsMargins(-1, 35, -1, -1)
        self.checkBox_31 = QCheckBox(self.tab_4)
        self.checkBox_31.setObjectName(u"checkBox_31")

        self.verticalLayout_24.addWidget(self.checkBox_31)

        self.checkBox_32 = QCheckBox(self.tab_4)
        self.checkBox_32.setObjectName(u"checkBox_32")

        self.verticalLayout_24.addWidget(self.checkBox_32)


        self.verticalLayout_3.addLayout(self.verticalLayout_24)


        self.horizontalLayout_34.addLayout(self.verticalLayout_3)


        self.verticalLayout_6.addLayout(self.horizontalLayout_34)

        self.horizontalLayout_47 = QHBoxLayout()
        self.horizontalLayout_47.setObjectName(u"horizontalLayout_47")
        self.horizontalLayout_47.setContentsMargins(-1, 0, -1, 40)
        self.pushButton_10 = QPushButton(self.tab_4)
        self.pushButton_10.setObjectName(u"pushButton_10")
        sizePolicy2 = QSizePolicy(QSizePolicy.Fixed, QSizePolicy.Fixed)
        sizePolicy2.setHorizontalStretch(0)
        sizePolicy2.setVerticalStretch(1)
        sizePolicy2.setHeightForWidth(self.pushButton_10.sizePolicy().hasHeightForWidth())
        self.pushButton_10.setSizePolicy(sizePolicy2)
        self.pushButton_10.setStyleSheet(u"background-color: rgb(255, 0, 0);")

        self.horizontalLayout_47.addWidget(self.pushButton_10)

        self.pushButton_19 = QPushButton(self.tab_4)
        self.pushButton_19.setObjectName(u"pushButton_19")
        sizePolicy.setHeightForWidth(self.pushButton_19.sizePolicy().hasHeightForWidth())
        self.pushButton_19.setSizePolicy(sizePolicy)
        self.pushButton_19.setStyleSheet(u"background-color: rgb(255, 0, 0);")

        self.horizontalLayout_47.addWidget(self.pushButton_19)

        self.pushButton_20 = QPushButton(self.tab_4)
        self.pushButton_20.setObjectName(u"pushButton_20")
        sizePolicy.setHeightForWidth(self.pushButton_20.sizePolicy().hasHeightForWidth())
        self.pushButton_20.setSizePolicy(sizePolicy)
        self.pushButton_20.setStyleSheet(u"background-color: rgb(255, 0, 0);")

        self.horizontalLayout_47.addWidget(self.pushButton_20)

        self.pushButton_21 = QPushButton(self.tab_4)
        self.pushButton_21.setObjectName(u"pushButton_21")
        sizePolicy.setHeightForWidth(self.pushButton_21.sizePolicy().hasHeightForWidth())
        self.pushButton_21.setSizePolicy(sizePolicy)
        self.pushButton_21.setStyleSheet(u"background-color: rgb(255, 0, 0);")

        self.horizontalLayout_47.addWidget(self.pushButton_21)


        self.verticalLayout_6.addLayout(self.horizontalLayout_47)

        self.horizontalLayout_3 = QHBoxLayout()
        self.horizontalLayout_3.setObjectName(u"horizontalLayout_3")
        self.horizontalSpacer_18 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_3.addItem(self.horizontalSpacer_18)

        self.pushButton_16 = QPushButton(self.tab_4)
        self.pushButton_16.setObjectName(u"pushButton_16")

        self.horizontalLayout_3.addWidget(self.pushButton_16)

        self.pushButton_17 = QPushButton(self.tab_4)
        self.pushButton_17.setObjectName(u"pushButton_17")

        self.horizontalLayout_3.addWidget(self.pushButton_17)

        self.pushButton_4 = QPushButton(self.tab_4)
        self.pushButton_4.setObjectName(u"pushButton_4")

        self.horizontalLayout_3.addWidget(self.pushButton_4)


        self.verticalLayout_6.addLayout(self.horizontalLayout_3)

        self.tabWidget.addTab(self.tab_4, "")
        self.tab_5 = QWidget()
        self.tab_5.setObjectName(u"tab_5")
        self.verticalLayout_17 = QVBoxLayout(self.tab_5)
        self.verticalLayout_17.setObjectName(u"verticalLayout_17")
        self.horizontalLayout_6 = QHBoxLayout()
        self.horizontalLayout_6.setObjectName(u"horizontalLayout_6")
        self.verticalLayout_13 = QVBoxLayout()
        self.verticalLayout_13.setObjectName(u"verticalLayout_13")
        self.horizontalLayout_9 = QHBoxLayout()
        self.horizontalLayout_9.setObjectName(u"horizontalLayout_9")
        self.horizontalLayout_9.setContentsMargins(0, -1, 49, -1)
        self.label_21 = QLabel(self.tab_5)
        self.label_21.setObjectName(u"label_21")

        self.horizontalLayout_9.addWidget(self.label_21)

        self.timeEdit = QTimeEdit(self.tab_5)
        self.timeEdit.setObjectName(u"timeEdit")
        sizePolicy.setHeightForWidth(self.timeEdit.sizePolicy().hasHeightForWidth())
        self.timeEdit.setSizePolicy(sizePolicy)

        self.horizontalLayout_9.addWidget(self.timeEdit)


        self.verticalLayout_13.addLayout(self.horizontalLayout_9)

        self.horizontalLayout_11 = QHBoxLayout()
        self.horizontalLayout_11.setObjectName(u"horizontalLayout_11")
        self.horizontalLayout_11.setContentsMargins(-1, -1, 50, -1)
        self.label_24 = QLabel(self.tab_5)
        self.label_24.setObjectName(u"label_24")

        self.horizontalLayout_11.addWidget(self.label_24)

        self.timeEdit_2 = QTimeEdit(self.tab_5)
        self.timeEdit_2.setObjectName(u"timeEdit_2")
        sizePolicy.setHeightForWidth(self.timeEdit_2.sizePolicy().hasHeightForWidth())
        self.timeEdit_2.setSizePolicy(sizePolicy)
        self.timeEdit_2.setMaximumTime(QTime(23, 59, 59))
        self.timeEdit_2.setTime(QTime(0, 0, 0))

        self.horizontalLayout_11.addWidget(self.timeEdit_2)


        self.verticalLayout_13.addLayout(self.horizontalLayout_11)

        self.horizontalLayout_4 = QHBoxLayout()
        self.horizontalLayout_4.setObjectName(u"horizontalLayout_4")
        self.horizontalLayout_4.setContentsMargins(-1, -1, 49, -1)
        self.label_33 = QLabel(self.tab_5)
        self.label_33.setObjectName(u"label_33")

        self.horizontalLayout_4.addWidget(self.label_33)

        self.timeEdit_6 = QTimeEdit(self.tab_5)
        self.timeEdit_6.setObjectName(u"timeEdit_6")
        sizePolicy.setHeightForWidth(self.timeEdit_6.sizePolicy().hasHeightForWidth())
        self.timeEdit_6.setSizePolicy(sizePolicy)

        self.horizontalLayout_4.addWidget(self.timeEdit_6)


        self.verticalLayout_13.addLayout(self.horizontalLayout_4)

        self.horizontalLayout_13 = QHBoxLayout()
        self.horizontalLayout_13.setObjectName(u"horizontalLayout_13")
        self.horizontalLayout_13.setContentsMargins(-1, -1, 49, -1)
        self.label_32 = QLabel(self.tab_5)
        self.label_32.setObjectName(u"label_32")

        self.horizontalLayout_13.addWidget(self.label_32)

        self.timeEdit_5 = QTimeEdit(self.tab_5)
        self.timeEdit_5.setObjectName(u"timeEdit_5")
        sizePolicy.setHeightForWidth(self.timeEdit_5.sizePolicy().hasHeightForWidth())
        self.timeEdit_5.setSizePolicy(sizePolicy)
        self.timeEdit_5.setDateTime(QDateTime(QDate(2000, 1, 1), QTime(0, 0, 0)))
        self.timeEdit_5.setMinimumTime(QTime(0, 0, 0))

        self.horizontalLayout_13.addWidget(self.timeEdit_5)

        self.horizontalLayout_13.setStretch(0, 1)

        self.verticalLayout_13.addLayout(self.horizontalLayout_13)

        self.horizontalLayout_16 = QHBoxLayout()
        self.horizontalLayout_16.setObjectName(u"horizontalLayout_16")
        self.horizontalLayout_16.setContentsMargins(-1, -1, 49, -1)
        self.label_28 = QLabel(self.tab_5)
        self.label_28.setObjectName(u"label_28")

        self.horizontalLayout_16.addWidget(self.label_28)

        self.timeEdit_3 = QTimeEdit(self.tab_5)
        self.timeEdit_3.setObjectName(u"timeEdit_3")
        sizePolicy.setHeightForWidth(self.timeEdit_3.sizePolicy().hasHeightForWidth())
        self.timeEdit_3.setSizePolicy(sizePolicy)

        self.horizontalLayout_16.addWidget(self.timeEdit_3)


        self.verticalLayout_13.addLayout(self.horizontalLayout_16)

        self.horizontalLayout_15 = QHBoxLayout()
        self.horizontalLayout_15.setObjectName(u"horizontalLayout_15")
        self.horizontalLayout_15.setContentsMargins(-1, -1, 50, -1)
        self.label_27 = QLabel(self.tab_5)
        self.label_27.setObjectName(u"label_27")

        self.horizontalLayout_15.addWidget(self.label_27)

        self.timeEdit_4 = QTimeEdit(self.tab_5)
        self.timeEdit_4.setObjectName(u"timeEdit_4")
        sizePolicy.setHeightForWidth(self.timeEdit_4.sizePolicy().hasHeightForWidth())
        self.timeEdit_4.setSizePolicy(sizePolicy)

        self.horizontalLayout_15.addWidget(self.timeEdit_4)

        self.horizontalLayout_15.setStretch(0, 1)

        self.verticalLayout_13.addLayout(self.horizontalLayout_15)


        self.horizontalLayout_6.addLayout(self.verticalLayout_13)

        self.horizontalSpacer_21 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_6.addItem(self.horizontalSpacer_21)

        self.verticalLayout_16 = QVBoxLayout()
        self.verticalLayout_16.setObjectName(u"verticalLayout_16")
        self.verticalLayout_16.setContentsMargins(-1, -1, 126, -1)
        self.verticalLayout_14 = QVBoxLayout()
        self.verticalLayout_14.setObjectName(u"verticalLayout_14")
        self.checkBox_2 = QCheckBox(self.tab_5)
        self.checkBox_2.setObjectName(u"checkBox_2")

        self.verticalLayout_14.addWidget(self.checkBox_2)

        self.checkBox = QCheckBox(self.tab_5)
        self.checkBox.setObjectName(u"checkBox")

        self.verticalLayout_14.addWidget(self.checkBox)

        self.checkBox_3 = QCheckBox(self.tab_5)
        self.checkBox_3.setObjectName(u"checkBox_3")

        self.verticalLayout_14.addWidget(self.checkBox_3)


        self.verticalLayout_16.addLayout(self.verticalLayout_14)

        self.verticalLayout_15 = QVBoxLayout()
        self.verticalLayout_15.setObjectName(u"verticalLayout_15")
        self.horizontalLayout_10 = QHBoxLayout()
        self.horizontalLayout_10.setObjectName(u"horizontalLayout_10")
        self.label_30 = QLabel(self.tab_5)
        self.label_30.setObjectName(u"label_30")

        self.horizontalLayout_10.addWidget(self.label_30)

        self.spinBox_19 = QSpinBox(self.tab_5)
        self.spinBox_19.setObjectName(u"spinBox_19")

        self.horizontalLayout_10.addWidget(self.spinBox_19)


        self.verticalLayout_15.addLayout(self.horizontalLayout_10)

        self.horizontalLayout_12 = QHBoxLayout()
        self.horizontalLayout_12.setObjectName(u"horizontalLayout_12")
        self.label_31 = QLabel(self.tab_5)
        self.label_31.setObjectName(u"label_31")

        self.horizontalLayout_12.addWidget(self.label_31)

        self.spinBox_22 = QSpinBox(self.tab_5)
        self.spinBox_22.setObjectName(u"spinBox_22")
        self.spinBox_22.setMaximum(99)
        self.spinBox_22.setSingleStep(1)

        self.horizontalLayout_12.addWidget(self.spinBox_22)


        self.verticalLayout_15.addLayout(self.horizontalLayout_12)

        self.horizontalLayout_5 = QHBoxLayout()
        self.horizontalLayout_5.setObjectName(u"horizontalLayout_5")
        self.horizontalLayout_5.setContentsMargins(-1, -1, 60, -1)
        self.pushButton_9 = QPushButton(self.tab_5)
        self.pushButton_9.setObjectName(u"pushButton_9")
        sizePolicy.setHeightForWidth(self.pushButton_9.sizePolicy().hasHeightForWidth())
        self.pushButton_9.setSizePolicy(sizePolicy)

        self.horizontalLayout_5.addWidget(self.pushButton_9)


        self.verticalLayout_15.addLayout(self.horizontalLayout_5)


        self.verticalLayout_16.addLayout(self.verticalLayout_15)


        self.horizontalLayout_6.addLayout(self.verticalLayout_16)


        self.verticalLayout_17.addLayout(self.horizontalLayout_6)

        self.horizontalLayout_14 = QHBoxLayout()
        self.horizontalLayout_14.setObjectName(u"horizontalLayout_14")
        self.horizontalSpacer_19 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_14.addItem(self.horizontalSpacer_19)

        self.pushButton_26 = QPushButton(self.tab_5)
        self.pushButton_26.setObjectName(u"pushButton_26")

        self.horizontalLayout_14.addWidget(self.pushButton_26)

        self.pushButton_25 = QPushButton(self.tab_5)
        self.pushButton_25.setObjectName(u"pushButton_25")

        self.horizontalLayout_14.addWidget(self.pushButton_25)

        self.pushButton_5 = QPushButton(self.tab_5)
        self.pushButton_5.setObjectName(u"pushButton_5")

        self.horizontalLayout_14.addWidget(self.pushButton_5)


        self.verticalLayout_17.addLayout(self.horizontalLayout_14)

        self.tabWidget.addTab(self.tab_5, "")
        self.tab_6 = QWidget()
        self.tab_6.setObjectName(u"tab_6")
        self.verticalLayout_12 = QVBoxLayout(self.tab_6)
        self.verticalLayout_12.setObjectName(u"verticalLayout_12")
        self.verticalLayout_10 = QVBoxLayout()
        self.verticalLayout_10.setObjectName(u"verticalLayout_10")
        self.horizontalLayout_40 = QHBoxLayout()
        self.horizontalLayout_40.setObjectName(u"horizontalLayout_40")
        self.verticalLayout_4 = QVBoxLayout()
        self.verticalLayout_4.setObjectName(u"verticalLayout_4")
        self.horizontalLayout_17 = QHBoxLayout()
        self.horizontalLayout_17.setObjectName(u"horizontalLayout_17")
        self.label_35 = QLabel(self.tab_6)
        self.label_35.setObjectName(u"label_35")

        self.horizontalLayout_17.addWidget(self.label_35)

        self.lineEdit_8 = QLineEdit(self.tab_6)
        self.lineEdit_8.setObjectName(u"lineEdit_8")

        self.horizontalLayout_17.addWidget(self.lineEdit_8)


        self.verticalLayout_4.addLayout(self.horizontalLayout_17)

        self.horizontalLayout_18 = QHBoxLayout()
        self.horizontalLayout_18.setObjectName(u"horizontalLayout_18")
        self.label_36 = QLabel(self.tab_6)
        self.label_36.setObjectName(u"label_36")

        self.horizontalLayout_18.addWidget(self.label_36)

        self.lineEdit = QLineEdit(self.tab_6)
        self.lineEdit.setObjectName(u"lineEdit")

        self.horizontalLayout_18.addWidget(self.lineEdit)


        self.verticalLayout_4.addLayout(self.horizontalLayout_18)

        self.horizontalLayout_19 = QHBoxLayout()
        self.horizontalLayout_19.setObjectName(u"horizontalLayout_19")
        self.label_37 = QLabel(self.tab_6)
        self.label_37.setObjectName(u"label_37")

        self.horizontalLayout_19.addWidget(self.label_37)

        self.lineEdit_2 = QLineEdit(self.tab_6)
        self.lineEdit_2.setObjectName(u"lineEdit_2")

        self.horizontalLayout_19.addWidget(self.lineEdit_2)


        self.verticalLayout_4.addLayout(self.horizontalLayout_19)

        self.horizontalLayout_20 = QHBoxLayout()
        self.horizontalLayout_20.setObjectName(u"horizontalLayout_20")
        self.label_38 = QLabel(self.tab_6)
        self.label_38.setObjectName(u"label_38")

        self.horizontalLayout_20.addWidget(self.label_38)

        self.lineEdit_3 = QLineEdit(self.tab_6)
        self.lineEdit_3.setObjectName(u"lineEdit_3")

        self.horizontalLayout_20.addWidget(self.lineEdit_3)


        self.verticalLayout_4.addLayout(self.horizontalLayout_20)

        self.horizontalLayout_21 = QHBoxLayout()
        self.horizontalLayout_21.setObjectName(u"horizontalLayout_21")
        self.label_39 = QLabel(self.tab_6)
        self.label_39.setObjectName(u"label_39")

        self.horizontalLayout_21.addWidget(self.label_39)

        self.lineEdit_4 = QLineEdit(self.tab_6)
        self.lineEdit_4.setObjectName(u"lineEdit_4")

        self.horizontalLayout_21.addWidget(self.lineEdit_4)


        self.verticalLayout_4.addLayout(self.horizontalLayout_21)

        self.horizontalLayout_22 = QHBoxLayout()
        self.horizontalLayout_22.setObjectName(u"horizontalLayout_22")
        self.label_40 = QLabel(self.tab_6)
        self.label_40.setObjectName(u"label_40")

        self.horizontalLayout_22.addWidget(self.label_40)

        self.lineEdit_5 = QLineEdit(self.tab_6)
        self.lineEdit_5.setObjectName(u"lineEdit_5")

        self.horizontalLayout_22.addWidget(self.lineEdit_5)


        self.verticalLayout_4.addLayout(self.horizontalLayout_22)

        self.horizontalLayout_23 = QHBoxLayout()
        self.horizontalLayout_23.setObjectName(u"horizontalLayout_23")
        self.label_41 = QLabel(self.tab_6)
        self.label_41.setObjectName(u"label_41")

        self.horizontalLayout_23.addWidget(self.label_41)

        self.lineEdit_6 = QLineEdit(self.tab_6)
        self.lineEdit_6.setObjectName(u"lineEdit_6")

        self.horizontalLayout_23.addWidget(self.lineEdit_6)


        self.verticalLayout_4.addLayout(self.horizontalLayout_23)

        self.horizontalLayout_24 = QHBoxLayout()
        self.horizontalLayout_24.setObjectName(u"horizontalLayout_24")
        self.label_42 = QLabel(self.tab_6)
        self.label_42.setObjectName(u"label_42")

        self.horizontalLayout_24.addWidget(self.label_42)

        self.lineEdit_7 = QLineEdit(self.tab_6)
        self.lineEdit_7.setObjectName(u"lineEdit_7")

        self.horizontalLayout_24.addWidget(self.lineEdit_7)


        self.verticalLayout_4.addLayout(self.horizontalLayout_24)


        self.horizontalLayout_40.addLayout(self.verticalLayout_4)

        self.verticalLayout_5 = QVBoxLayout()
        self.verticalLayout_5.setObjectName(u"verticalLayout_5")
        self.horizontalLayout_25 = QHBoxLayout()
        self.horizontalLayout_25.setObjectName(u"horizontalLayout_25")
        self.label_43 = QLabel(self.tab_6)
        self.label_43.setObjectName(u"label_43")

        self.horizontalLayout_25.addWidget(self.label_43)

        self.lineEdit_9 = QLineEdit(self.tab_6)
        self.lineEdit_9.setObjectName(u"lineEdit_9")

        self.horizontalLayout_25.addWidget(self.lineEdit_9)


        self.verticalLayout_5.addLayout(self.horizontalLayout_25)

        self.horizontalLayout_26 = QHBoxLayout()
        self.horizontalLayout_26.setObjectName(u"horizontalLayout_26")
        self.label_44 = QLabel(self.tab_6)
        self.label_44.setObjectName(u"label_44")

        self.horizontalLayout_26.addWidget(self.label_44)

        self.lineEdit_10 = QLineEdit(self.tab_6)
        self.lineEdit_10.setObjectName(u"lineEdit_10")

        self.horizontalLayout_26.addWidget(self.lineEdit_10)


        self.verticalLayout_5.addLayout(self.horizontalLayout_26)

        self.horizontalLayout_27 = QHBoxLayout()
        self.horizontalLayout_27.setObjectName(u"horizontalLayout_27")
        self.label_45 = QLabel(self.tab_6)
        self.label_45.setObjectName(u"label_45")

        self.horizontalLayout_27.addWidget(self.label_45)

        self.lineEdit_11 = QLineEdit(self.tab_6)
        self.lineEdit_11.setObjectName(u"lineEdit_11")

        self.horizontalLayout_27.addWidget(self.lineEdit_11)


        self.verticalLayout_5.addLayout(self.horizontalLayout_27)

        self.horizontalLayout_28 = QHBoxLayout()
        self.horizontalLayout_28.setObjectName(u"horizontalLayout_28")
        self.label_46 = QLabel(self.tab_6)
        self.label_46.setObjectName(u"label_46")

        self.horizontalLayout_28.addWidget(self.label_46)

        self.lineEdit_12 = QLineEdit(self.tab_6)
        self.lineEdit_12.setObjectName(u"lineEdit_12")

        self.horizontalLayout_28.addWidget(self.lineEdit_12)


        self.verticalLayout_5.addLayout(self.horizontalLayout_28)

        self.horizontalLayout_29 = QHBoxLayout()
        self.horizontalLayout_29.setObjectName(u"horizontalLayout_29")
        self.label_47 = QLabel(self.tab_6)
        self.label_47.setObjectName(u"label_47")

        self.horizontalLayout_29.addWidget(self.label_47)

        self.lineEdit_13 = QLineEdit(self.tab_6)
        self.lineEdit_13.setObjectName(u"lineEdit_13")

        self.horizontalLayout_29.addWidget(self.lineEdit_13)


        self.verticalLayout_5.addLayout(self.horizontalLayout_29)

        self.horizontalLayout_30 = QHBoxLayout()
        self.horizontalLayout_30.setObjectName(u"horizontalLayout_30")
        self.label_48 = QLabel(self.tab_6)
        self.label_48.setObjectName(u"label_48")

        self.horizontalLayout_30.addWidget(self.label_48)

        self.lineEdit_14 = QLineEdit(self.tab_6)
        self.lineEdit_14.setObjectName(u"lineEdit_14")

        self.horizontalLayout_30.addWidget(self.lineEdit_14)


        self.verticalLayout_5.addLayout(self.horizontalLayout_30)

        self.horizontalLayout_31 = QHBoxLayout()
        self.horizontalLayout_31.setObjectName(u"horizontalLayout_31")
        self.label_49 = QLabel(self.tab_6)
        self.label_49.setObjectName(u"label_49")

        self.horizontalLayout_31.addWidget(self.label_49)

        self.lineEdit_15 = QLineEdit(self.tab_6)
        self.lineEdit_15.setObjectName(u"lineEdit_15")

        self.horizontalLayout_31.addWidget(self.lineEdit_15)


        self.verticalLayout_5.addLayout(self.horizontalLayout_31)

        self.horizontalLayout_32 = QHBoxLayout()
        self.horizontalLayout_32.setObjectName(u"horizontalLayout_32")
        self.label_50 = QLabel(self.tab_6)
        self.label_50.setObjectName(u"label_50")

        self.horizontalLayout_32.addWidget(self.label_50)

        self.lineEdit_16 = QLineEdit(self.tab_6)
        self.lineEdit_16.setObjectName(u"lineEdit_16")

        self.horizontalLayout_32.addWidget(self.lineEdit_16)


        self.verticalLayout_5.addLayout(self.horizontalLayout_32)


        self.horizontalLayout_40.addLayout(self.verticalLayout_5)

        self.horizontalSpacer_24 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_40.addItem(self.horizontalSpacer_24)


        self.verticalLayout_10.addLayout(self.horizontalLayout_40)

        self.horizontalLayout_39 = QHBoxLayout()
        self.horizontalLayout_39.setObjectName(u"horizontalLayout_39")
        self.horizontalLayout_39.setContentsMargins(-1, 29, -1, -1)
        self.horizontalSpacer_20 = QSpacerItem(40, 20, QSizePolicy.Expanding, QSizePolicy.Minimum)

        self.horizontalLayout_39.addItem(self.horizontalSpacer_20)

        self.pushButton_27 = QPushButton(self.tab_6)
        self.pushButton_27.setObjectName(u"pushButton_27")

        self.horizontalLayout_39.addWidget(self.pushButton_27)

        self.pushButton_28 = QPushButton(self.tab_6)
        self.pushButton_28.setObjectName(u"pushButton_28")

        self.horizontalLayout_39.addWidget(self.pushButton_28)

        self.pushButton_6 = QPushButton(self.tab_6)
        self.pushButton_6.setObjectName(u"pushButton_6")

        self.horizontalLayout_39.addWidget(self.pushButton_6)


        self.verticalLayout_10.addLayout(self.horizontalLayout_39)


        self.verticalLayout_12.addLayout(self.verticalLayout_10)

        self.tabWidget.addTab(self.tab_6, "")

        self.verticalLayout_11.addWidget(self.tabWidget)


        self.retranslateUi(Form)

        self.tabWidget.setCurrentIndex(1)


        QMetaObject.connectSlotsByName(Form)
    # setupUi

    def retranslateUi(self, Form):
        Form.setWindowTitle(QCoreApplication.translate("Form", u"Settings", None))
        self.checkBox_18.setText(QCoreApplication.translate("Form", u"\u542f\u7528\u76f8\u673a1", None))
        self.checkBox_19.setText(QCoreApplication.translate("Form", u"\u542f\u7528\u76f8\u673a2", None))
        self.checkBox_20.setText(QCoreApplication.translate("Form", u"\u542f\u7528\u76f8\u673a3", None))
        self.checkBox_21.setText(QCoreApplication.translate("Form", u"\u542f\u7528\u76f8\u673a4", None))
        self.checkBox_22.setText(QCoreApplication.translate("Form", u"\u542f\u7528\u76f8\u673a5", None))
        self.checkBox_23.setText(QCoreApplication.translate("Form", u"\u542f\u7528\u76f8\u673a6", None))
        self.checkBox_24.setText(QCoreApplication.translate("Form", u"\u542f\u7528\u76f8\u673a7", None))
        self.checkBox_25.setText(QCoreApplication.translate("Form", u"\u542f\u7528\u76f8\u673a8", None))
        self.checkBox_26.setText(QCoreApplication.translate("Form", u"\u542f\u7528\u76f8\u673a9", None))
        self.checkBox_30.setText(QCoreApplication.translate("Form", u"\u542f\u7528\u76f8\u673a10", None))
        self.label_10.setText(QCoreApplication.translate("Form", u"\u542f\u7528\u4e0b\u6599\u53e3\u4e2a\u6570", None))
        self.label_9.setText(QCoreApplication.translate("Form", u"\u76f8\u673a\u4e2a\u6570", None))
        self.pushButton_8.setText(QCoreApplication.translate("Form", u"\u4fdd\u5b58", None))
        self.pushButton_11.setText(QCoreApplication.translate("Form", u"\u53d6\u6d88", None))
        self.pushButton.setText(QCoreApplication.translate("Form", u"\u9000\u51fa", None))
        self.tabWidget.setTabText(self.tabWidget.indexOf(self.tab), QCoreApplication.translate("Form", u"\u76f8\u673a\u529f\u80fd\u9009\u62e9", None))
        self.label_12.setText(QCoreApplication.translate("Form", u"\u76f8\u673a1\u5b58\u50a8", None))
        self.lineEdit_17.setPlaceholderText(QCoreApplication.translate("Form", u"\u8bf7\u8f93\u5165\u6570\u636e\u5bc4\u5b58\u5668", None))
        self.pushButton_7.setText(QCoreApplication.translate("Form", u"\u52a0", None))
        self.pushButton_33.setText(QCoreApplication.translate("Form", u"\u51cf", None))
        self.label_11.setText(QCoreApplication.translate("Form", u"\u76f8\u673a2\u5b58\u50a8", None))
        self.lineEdit_19.setText("")
        self.lineEdit_19.setPlaceholderText(QCoreApplication.translate("Form", u"\u8bf7\u8f93\u5165\u6570\u636e\u5bc4\u5b58\u5668", None))
        self.pushButton_34.setText(QCoreApplication.translate("Form", u"\u52a0", None))
        self.pushButton_35.setText(QCoreApplication.translate("Form", u"\u51cf", None))
        self.label_13.setText(QCoreApplication.translate("Form", u"\u76f8\u673a3\u5b58\u50a8", None))
        self.lineEdit_21.setPlaceholderText(QCoreApplication.translate("Form", u"\u8bf7\u8f93\u5165\u6570\u636e\u5bc4\u5b58\u5668", None))
        self.pushButton_36.setText(QCoreApplication.translate("Form", u"\u52a0", None))
        self.pushButton_37.setText(QCoreApplication.translate("Form", u"\u51cf", None))
        self.label_14.setText(QCoreApplication.translate("Form", u"\u76f8\u673a4\u5b58\u50a8", None))
        self.lineEdit_23.setPlaceholderText(QCoreApplication.translate("Form", u"\u8bf7\u8f93\u5165\u6570\u636e\u5bc4\u5b58\u5668", None))
        self.pushButton_38.setText(QCoreApplication.translate("Form", u"\u52a0", None))
        self.pushButton_39.setText(QCoreApplication.translate("Form", u"\u51cf", None))
        self.label_15.setText(QCoreApplication.translate("Form", u"\u76f8\u673a5\u5b58\u50a8", None))
        self.lineEdit_25.setPlaceholderText(QCoreApplication.translate("Form", u"\u8bf7\u8f93\u5165\u6570\u636e\u5bc4\u5b58\u5668", None))
        self.pushButton_40.setText(QCoreApplication.translate("Form", u"\u52a0", None))
        self.pushButton_41.setText(QCoreApplication.translate("Form", u"\u51cf", None))
        self.label_16.setText(QCoreApplication.translate("Form", u"\u76f8\u673a6\u5b58\u50a8", None))
        self.lineEdit_27.setPlaceholderText(QCoreApplication.translate("Form", u"\u8bf7\u8f93\u5165\u6570\u636e\u5bc4\u5b58\u5668", None))
        self.pushButton_42.setText(QCoreApplication.translate("Form", u"\u52a0", None))
        self.pushButton_43.setText(QCoreApplication.translate("Form", u"\u51cf", None))
        self.label_18.setText(QCoreApplication.translate("Form", u"\u76f8\u673a7\u5b58\u50a8", None))
        self.lineEdit_18.setPlaceholderText(QCoreApplication.translate("Form", u"\u8bf7\u8f93\u5165\u6570\u636e\u5bc4\u5b58\u5668", None))
        self.pushButton_44.setText(QCoreApplication.translate("Form", u"\u52a0", None))
        self.pushButton_45.setText(QCoreApplication.translate("Form", u"\u51cf", None))
        self.label_17.setText(QCoreApplication.translate("Form", u"\u76f8\u673a8\u5b58\u50a8", None))
        self.lineEdit_20.setPlaceholderText(QCoreApplication.translate("Form", u"\u8bf7\u8f93\u5165\u6570\u636e\u5bc4\u5b58\u5668", None))
        self.pushButton_46.setText(QCoreApplication.translate("Form", u"\u52a0", None))
        self.pushButton_47.setText(QCoreApplication.translate("Form", u"\u51cf", None))
        self.label.setText(QCoreApplication.translate("Form", u"\u4f4d\u7f6e\u52a0\u51cf\u57fa\u6570", None))
        self.pushButton_12.setText(QCoreApplication.translate("Form", u"\u4fdd\u5b58", None))
        self.pushButton_13.setText(QCoreApplication.translate("Form", u"\u53d6\u6d88", None))
        self.pushButton_2.setText(QCoreApplication.translate("Form", u"\u9000\u51fa", None))
        self.tabWidget.setTabText(self.tabWidget.indexOf(self.tab_2), QCoreApplication.translate("Form", u"\u76f8\u673a\u5b58\u50a8\u4f4d\u7f6e", None))
        self.label_4.setText(QCoreApplication.translate("Form", u"\u76f8\u673a9\u5b58\u50a8", None))
        self.lineEdit_32.setPlaceholderText(QCoreApplication.translate("Form", u"\u8bf7\u8f93\u5165\u6570\u636e\u5bc4\u5b58\u5668", None))
        self.pushButton_48.setText(QCoreApplication.translate("Form", u"\u52a0", None))
        self.pushButton_49.setText(QCoreApplication.translate("Form", u"\u51cf", None))
        self.label_8.setText(QCoreApplication.translate("Form", u"\u76f8\u673a10\u5b58\u50a8", None))
        self.lineEdit_66.setPlaceholderText(QCoreApplication.translate("Form", u"\u8bf7\u8f93\u5165\u6570\u636e\u5bc4\u5b58\u5668", None))
        self.pushButton_50.setText(QCoreApplication.translate("Form", u"\u52a0", None))
        self.pushButton_51.setText(QCoreApplication.translate("Form", u"\u51cf", None))
        self.label_85.setText(QCoreApplication.translate("Form", u"\u5439\u6c141\u5b58\u50a8", None))
        self.lineEdit_63.setPlaceholderText(QCoreApplication.translate("Form", u"\u8bf7\u8f93\u5165\u6570\u636e\u5bc4\u5b58\u5668", None))
        self.pushButton_52.setText(QCoreApplication.translate("Form", u"\u52a0", None))
        self.pushButton_53.setText(QCoreApplication.translate("Form", u"\u51cf", None))
        self.label_86.setText(QCoreApplication.translate("Form", u"\u5439\u6c142\u5b58\u50a8", None))
        self.lineEdit_65.setPlaceholderText(QCoreApplication.translate("Form", u"\u8bf7\u8f93\u5165\u6570\u636e\u5bc4\u5b58\u5668", None))
        self.pushButton_54.setText(QCoreApplication.translate("Form", u"\u52a0", None))
        self.pushButton_55.setText(QCoreApplication.translate("Form", u"\u51cf", None))
        self.label_87.setText(QCoreApplication.translate("Form", u"\u5439\u6c143\u5b58\u50a8", None))
        self.lineEdit_67.setPlaceholderText(QCoreApplication.translate("Form", u"\u8bf7\u8f93\u5165\u6570\u636e\u5bc4\u5b58\u5668", None))
        self.pushButton_56.setText(QCoreApplication.translate("Form", u"\u52a0", None))
        self.pushButton_57.setText(QCoreApplication.translate("Form", u"\u51cf", None))
        self.label_89.setText(QCoreApplication.translate("Form", u"\u5439\u6c144\u5b58\u50a8", None))
        self.lineEdit_64.setPlaceholderText(QCoreApplication.translate("Form", u"\u8bf7\u8f93\u5165\u6570\u636e\u5bc4\u5b58\u5668", None))
        self.pushButton_58.setText(QCoreApplication.translate("Form", u"\u52a0", None))
        self.pushButton_59.setText(QCoreApplication.translate("Form", u"\u51cf", None))
        self.label_88.setText(QCoreApplication.translate("Form", u"\u5439\u6c145\u5b58\u50a8", None))
        self.lineEdit_68.setPlaceholderText(QCoreApplication.translate("Form", u"\u8bf7\u8f93\u5165\u6570\u636e\u5bc4\u5b58\u5668", None))
        self.pushButton_60.setText(QCoreApplication.translate("Form", u"\u52a0", None))
        self.pushButton_61.setText(QCoreApplication.translate("Form", u"\u51cf", None))
        self.label_2.setText(QCoreApplication.translate("Form", u"\u4f4d\u7f6e\u52a0\u51cf\u57fa\u6570", None))
        self.pushButton_14.setText(QCoreApplication.translate("Form", u"\u4fdd\u5b58", None))
        self.pushButton_15.setText(QCoreApplication.translate("Form", u"\u53d6\u6d88", None))
        self.pushButton_3.setText(QCoreApplication.translate("Form", u"\u9000\u51fa", None))
        self.tabWidget.setTabText(self.tabWidget.indexOf(self.tab_3), QCoreApplication.translate("Form", u"\u76f8\u673a\u5b58\u50a8\u4f4d\u7f6e", None))
        self.checkBox_6.setText(QCoreApplication.translate("Form", u"\u5b9a\u4f4d\u76f8\u673a1", None))
        self.checkBox_7.setText(QCoreApplication.translate("Form", u"\u5b9a\u4f4d\u76f8\u673a2", None))
        self.checkBox_8.setText(QCoreApplication.translate("Form", u"\u5b9a\u4f4d\u76f8\u673a3", None))
        self.checkBox_9.setText(QCoreApplication.translate("Form", u"\u5b9a\u4f4d\u76f8\u673a4", None))
        self.checkBox_10.setText(QCoreApplication.translate("Form", u"\u5b9a\u4f4d\u76f8\u673a5", None))
        self.checkBox_11.setText(QCoreApplication.translate("Form", u"\u5b9a\u4f4d\u76f8\u673a6", None))
        self.checkBox_12.setText(QCoreApplication.translate("Form", u"\u5b9a\u4f4d\u76f8\u673a7", None))
        self.checkBox_13.setText(QCoreApplication.translate("Form", u"\u5b9a\u4f4d\u76f8\u673a8", None))
        self.checkBox_28.setText(QCoreApplication.translate("Form", u"\u5b9a\u4f4d\u76f8\u673a9", None))
        self.checkBox_29.setText(QCoreApplication.translate("Form", u"\u5b9a\u4f4d\u76f8\u673a10", None))
        self.checkBox_14.setText(QCoreApplication.translate("Form", u"\u5b9a\u4f4d\u5439\u6c141", None))
        self.checkBox_15.setText(QCoreApplication.translate("Form", u"\u5b9a\u4f4d\u5439\u6c142", None))
        self.checkBox_16.setText(QCoreApplication.translate("Form", u"\u5b9a\u4f4d\u5439\u6c143", None))
        self.checkBox_17.setText(QCoreApplication.translate("Form", u"\u5b9a\u4f4d\u5439\u6c144", None))
        self.checkBox_27.setText(QCoreApplication.translate("Form", u"\u5b9a\u4f4d\u5439\u6c145", None))
        self.pushButton_22.setText(QCoreApplication.translate("Form", u"\u6d4b\u8bd5\u5439\u6c14", None))
        self.pushButton_23.setText(QCoreApplication.translate("Form", u"\u5b9a\u4f4d\u5b8c\u6210", None))
        self.pushButton_24.setText(QCoreApplication.translate("Form", u"\u4e0b\u4e00\u5b9a\u4f4d", None))
        self.checkBox_31.setText(QCoreApplication.translate("Form", u"\u5b9a\u4f4d", None))
        self.checkBox_32.setText(QCoreApplication.translate("Form", u"\u5ffd\u7565\u6c14\u538b", None))
        self.pushButton_10.setText(QCoreApplication.translate("Form", u"\u624b\u52a8\u6b63\u8f6c", None))
        self.pushButton_19.setText(QCoreApplication.translate("Form", u"\u624b\u52a8\u5fae\u6b63\u8f6c", None))
        self.pushButton_20.setText(QCoreApplication.translate("Form", u"\u624b\u52a8\u53cd\u8f6c", None))
        self.pushButton_21.setText(QCoreApplication.translate("Form", u"\u624b\u52a8\u5fae\u53cd\u8f6c", None))
        self.pushButton_16.setText(QCoreApplication.translate("Form", u"\u4fdd\u5b58", None))
        self.pushButton_17.setText(QCoreApplication.translate("Form", u"\u53d6\u6d88", None))
        self.pushButton_4.setText(QCoreApplication.translate("Form", u"\u9000\u51fa", None))
        self.tabWidget.setTabText(self.tabWidget.indexOf(self.tab_4), QCoreApplication.translate("Form", u"\u8f6c\u76d8\u64cd\u4f5c", None))
        self.label_21.setText(QCoreApplication.translate("Form", u"\u5ef6\u65f6\u542f\u52a8\u632f\u52a8\u76d8", None))
        self.timeEdit.setDisplayFormat(QCoreApplication.translate("Form", u"mm:ss:zzz", None))
        self.label_24.setText(QCoreApplication.translate("Form", u"\u5ef6\u65f6\u505c\u673a", None))
        self.timeEdit_2.setDisplayFormat(QCoreApplication.translate("Form", u"mm:ss:zzz", None))
        self.label_33.setText(QCoreApplication.translate("Form", u"\u957f\u65f6\u95f4\u65e0\u6599\u505c\u673a\u65f6\u95f4\u8bbe\u5b9a", None))
        self.timeEdit_6.setDisplayFormat(QCoreApplication.translate("Form", u"mm:ss:zzz", None))
        self.label_32.setText(QCoreApplication.translate("Form", u"\u957f\u65f6\u95f4\u65e0\u6599\u62a5\u8b66\u65f6\u95f4\u8bbe\u5b9a", None))
        self.timeEdit_5.setDisplayFormat(QCoreApplication.translate("Form", u"mm:ss:zzz", None))
        self.label_28.setText(QCoreApplication.translate("Form", u"\u632f\u52a8\u76d8\u65e0\u6599\u5ef6\u65f6\u505c\u6b62\u722c\u5761\u673a", None))
        self.timeEdit_3.setDisplayFormat(QCoreApplication.translate("Form", u"mm:ss:zzz", None))
        self.label_27.setText(QCoreApplication.translate("Form", u"\u632f\u52a8\u76d8\u6ee1\u6599\u5ef6\u65f6\u542f\u52a8\u722c\u5761\u673a", None))
        self.timeEdit_4.setDisplayFormat(QCoreApplication.translate("Form", u"mm:ss:zzz", None))
        self.checkBox_2.setText(QCoreApplication.translate("Form", u"\u542f\u52a8\u53cc\u632f\u52a8\u76d8", None))
        self.checkBox.setText(QCoreApplication.translate("Form", u"\u542f\u7528\u722c\u5761\u673a", None))
        self.checkBox_3.setText(QCoreApplication.translate("Form", u"\u542f\u52a8\u5305\u88c5\u8ba1\u6570", None))
        self.label_30.setText(QCoreApplication.translate("Form", u"\u5305\u88c5\u8ba1\u6570\u6570\u636e\u8bbe\u5b9a", None))
        self.label_31.setText(QCoreApplication.translate("Form", u"\u5f53\u524d\u5305\u88c5\u6570\u91cf", None))
        self.pushButton_9.setText(QCoreApplication.translate("Form", u"\u5305\u88c5\u8ba1\u6570\u6e05\u96f6", None))
        self.pushButton_26.setText(QCoreApplication.translate("Form", u"\u4fdd\u5b58", None))
        self.pushButton_25.setText(QCoreApplication.translate("Form", u"\u53d6\u6d88", None))
        self.pushButton_5.setText(QCoreApplication.translate("Form", u"\u9000\u51fa", None))
        self.tabWidget.setTabText(self.tabWidget.indexOf(self.tab_5), QCoreApplication.translate("Form", u"\u989d\u5916\u64cd\u4f5c\u6570\u636e\u8bbe\u5b9a", None))
        self.label_35.setText(QCoreApplication.translate("Form", u"R1000", None))
        self.lineEdit_8.setPlaceholderText(QCoreApplication.translate("Form", u"\u6545\u969c\u540d\u79f0", None))
        self.label_36.setText(QCoreApplication.translate("Form", u"R1001", None))
        self.lineEdit.setPlaceholderText(QCoreApplication.translate("Form", u"\u6545\u969c\u540d\u79f0", None))
        self.label_37.setText(QCoreApplication.translate("Form", u"R1002", None))
        self.lineEdit_2.setText("")
        self.lineEdit_2.setPlaceholderText(QCoreApplication.translate("Form", u"\u6545\u969c\u540d\u79f0", None))
        self.label_38.setText(QCoreApplication.translate("Form", u"R1003", None))
        self.lineEdit_3.setPlaceholderText(QCoreApplication.translate("Form", u"\u6545\u969c\u540d\u79f0", None))
        self.label_39.setText(QCoreApplication.translate("Form", u"R1004", None))
        self.lineEdit_4.setPlaceholderText(QCoreApplication.translate("Form", u"\u6545\u969c\u540d\u79f0", None))
        self.label_40.setText(QCoreApplication.translate("Form", u"R1005", None))
        self.lineEdit_5.setPlaceholderText(QCoreApplication.translate("Form", u"\u6545\u969c\u540d\u79f0", None))
        self.label_41.setText(QCoreApplication.translate("Form", u"R1006", None))
        self.lineEdit_6.setPlaceholderText(QCoreApplication.translate("Form", u"\u6545\u969c\u540d\u79f0", None))
        self.label_42.setText(QCoreApplication.translate("Form", u"R1007", None))
        self.lineEdit_7.setPlaceholderText(QCoreApplication.translate("Form", u"\u6545\u969c\u540d\u79f0", None))
        self.label_43.setText(QCoreApplication.translate("Form", u"R1009", None))
        self.lineEdit_9.setPlaceholderText(QCoreApplication.translate("Form", u"\u6545\u969c\u540d\u79f0", None))
        self.label_44.setText(QCoreApplication.translate("Form", u"R101A", None))
        self.lineEdit_10.setPlaceholderText(QCoreApplication.translate("Form", u"\u6545\u969c\u540d\u79f0", None))
        self.label_45.setText(QCoreApplication.translate("Form", u"R101B", None))
        self.lineEdit_11.setPlaceholderText(QCoreApplication.translate("Form", u"\u6545\u969c\u540d\u79f0", None))
        self.label_46.setText(QCoreApplication.translate("Form", u"R101C", None))
        self.lineEdit_12.setPlaceholderText(QCoreApplication.translate("Form", u"\u6545\u969c\u540d\u79f0", None))
        self.label_47.setText(QCoreApplication.translate("Form", u"R101D", None))
        self.lineEdit_13.setPlaceholderText(QCoreApplication.translate("Form", u"\u6545\u969c\u540d\u79f0", None))
        self.label_48.setText(QCoreApplication.translate("Form", u"R101E", None))
        self.lineEdit_14.setPlaceholderText(QCoreApplication.translate("Form", u"\u6545\u969c\u540d\u79f0", None))
        self.label_49.setText(QCoreApplication.translate("Form", u"R101F", None))
        self.lineEdit_15.setPlaceholderText(QCoreApplication.translate("Form", u"\u6545\u969c\u540d\u79f0", None))
        self.label_50.setText(QCoreApplication.translate("Form", u"R1020", None))
        self.lineEdit_16.setPlaceholderText(QCoreApplication.translate("Form", u"\u6545\u969c\u540d\u79f0", None))
        self.pushButton_27.setText(QCoreApplication.translate("Form", u"\u6545\u969c\u786e\u8ba4", None))
        self.pushButton_28.setText(QCoreApplication.translate("Form", u"\u53d6\u6d88", None))
        self.pushButton_6.setText(QCoreApplication.translate("Form", u"\u9000\u51fa", None))
        self.tabWidget.setTabText(self.tabWidget.indexOf(self.tab_6), QCoreApplication.translate("Form", u"\u6545\u969c\u8bbe\u5b9a", None))
    # retranslateUi

