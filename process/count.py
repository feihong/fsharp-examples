import sys
import time
import itertools

max = int(sys.argv[1]) if len(sys.argv) > 1 else 10
for i in itertools.count(1):
    print(i)
    time.sleep(0.5)
    if i == max:
        break
