from dnslib.server import DNSServer, DNSRecord, RR, QTYPE
from dnlib.dns import A

IP = "127.0.0.1"
PORT = 53
TTL = 60 * 5


#Make dns server reply
class DnsFlare():
    def resolveDns(self, request: DNSRecord, handler):
        
       


#https://snippets.cacher.io/snippet/3a345e8dec90c87abdb8#F0
def main():
    s = DNSServer(DnsFlare(), IP, PORT)




if __name__ == '__main__':
	main()