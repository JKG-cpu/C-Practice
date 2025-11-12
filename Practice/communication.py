import sys
from threading import Thread

class Communication:
    def __init__(self):
        self.running = True

    def receive_messages(self):
        while self.running:
            message = sys.stdin.readline().strip()
            if message:
                # Print the message received
                print(f"Python received: {message}")
                sys.stdout.flush()

                # Send response back to C#
                print(f"Hello C#, I got your message: {message}")
                sys.stdout.flush()
            else:
                # If empty, just wait a bit
                continue

    def main(self):
        # Start receiving messages in a thread
        Thread(target=self.receive_messages, daemon=True).start()

        # Initial message
        print("Python: Ready to receive messages...")
        sys.stdout.flush()

        # Keep main thread alive
        try:
            while self.running:
                pass
        except KeyboardInterrupt:
            self.running = False

if __name__ == "__main__":
    Communication().main()
