import secrets
import string
import json
from sys import argv

def generate_password(length: int) -> str:
    characters = string.ascii_letters + string.ascii_lowercase + string.ascii_uppercase + string.digits + string.punctuation
    return ''.join(secrets.choice(characters) for _ in range(length))

if __name__ == '__main__':
    number = 16
    if len(argv) > 1:
        text = argv[1]
        if text.isdigit():
            number = int(text)
    
    password = generate_password(number)

    print(json.dumps({"Password": password}))