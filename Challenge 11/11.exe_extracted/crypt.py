class ARC4(object):
    def __init__(self, key: bytes):
        self.key = key
        print("Key is: " + key.decode("utf-8"))
    def encrypt(self, data: bytes):
        print("Data encrypt is: " + data.decode("utf-8"))
    def decrypt(self, data: bytes):
        print("Data decrypt is: " + data.decode("utf-8"))