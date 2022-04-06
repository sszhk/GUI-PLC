import torch
import torch.nn as nn
import torch.nn.functional as F
import torch.optim as optim
import torchvision
import torchvision.transforms as transforms

transform=transforms.Compose(
    [transforms.ToTensor(),#PIL Image转为 tensor
     transforms.Normalize((0.5,0.5,0.5),(0.5,0.5,0.5))]#0,1转为-1，1
)
torch.utils.data.DataLoader
class Net(nn.Module):
    def __init__(self):
        super(Net,self).__init__()
        self.conv1=nn.Conv2d(3,6,5)
        self.pool=nn.MaxPool2d(2,2)
        self.conv2=nn.Conv2d(6,16,5)
        nn.Linear()
