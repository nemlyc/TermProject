import socket
import time
import datetime
import threading
from time import sleep

host = '127.0.0.1'
port = 50000

tello_ip = '192.168.10.1'
tello_port = 8889
tello_address = (tello_ip, tello_port)


judge = ''
# for receiving
class TestThread(threading.Thread):
    def __init__(self,v=''):
        super(TestThread, self).__init__()

        self.host = "127.0.0.1"
        self.port = 50000
        self.backlog = 10
        self.bufsize = 1024
        # --ドローンのための変数--
        self.message = v
        # ------------------------
        self.sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
        self.sock.bind((self.host, self.port))

    def run(self):
        print (" === sub thread === ")
        print ('クライアントから受信をする')
        while True:
            data, addr = self.sock.recvfrom(self.bufsize)
            seikei = (data.decode()).split(',')
            self.message = seikei[0]
            print('命令を受け付けました: order is ',self.message)
            if data.decode() == 'q':
                print ("sub thread is being terminaited")
                break
            print ('main thread message is ',self.message)

        self.sock.close()


if __name__ == '__main__':
    th = TestThread()
    th.setDaemon(True)
    th.start()

    time.sleep(1)
    # time.sleep(100)  # これは長すぎる（１００秒待て、という指令）

    print(" === main thread === ")

    ip = "127.0.0.1"
    port = 55555
    bufsize = 1024

    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    sock.sendto('command'.encode('utf-8'), tello_address)
    print('set command mode\r\n')
    time.sleep(5)  # wait 5sec

    # takeoff
    sock.sendto('takeoff'.encode('utf-8'), tello_address)
    print('takeoff\r\n')
    time.sleep(10)  # wait 10sec

    while True:
        if th.message == '':
            continue
        elif th.message == 'up':
            sock.sendto('up 20'.encode('utf-8'),tello_address)
            th.message = ''
            sleep(5)
        elif th.message == 'down':
            sock.sendto('down 20'.encode('utf-8'),tello_address)
            th.message = ''
            sleep(5)
        elif th.message == 'front':
            sock.sendto('forward 20'.encode('utf-8'),tello_address)
            th.message = ''
            sleep(5)
        elif th.message == 'back':
            sock.sendto('back 20'.encode('utf-8'),tello_address)
            th.message = ''
            sleep(5)
        elif th.message == 'right':
            sock.sendto('right 20'.encode('utf-8'),tello_address)
            th.message = ''
            sleep(5)
        elif th.message == 'left':
            sock.sendto('left 20'.encode('utf-8'),tello_address)
            th.message = ''
            sleep(5)
        elif th.message == 'turn_right':
            sock.sendto('cw 45'.encode('utf-8'), tello_address)
            th.message = ''
            sleep(5)
        elif th.message=='turn_left':
            sock.sendto('cw -45'.encode('utf-8'), tello_address)
            th.message = ''
            sleep(5)
        elif th.message =='land':
            sock.sendto('land'.encode('utf-8'), tello_address)
            break
        else:
            continue
        
        time.sleep(1)
        # if inp == 'q':
        #     th.join()
        #     print("main thread is being terminated")
        #     break

    sock.close()
