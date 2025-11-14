# talk.py
import sys

print("🐍 Python: Ready and waiting for message...")
sys.stdout.flush()  # Force immediate print output

# Read message from stdin (sent by C#)
incoming = sys.stdin.readline().strip()
print(f"🐍 Python: Received from C#: {incoming}")

# Send a response back
response = f"Hello C#, I got your message: '{incoming}'!"
print("🐍 Python: Sending response...")
sys.stdout.flush()

# Actually return the message to C# through stdout
print(response)
sys.stdout.flush()
