import requests

base_url = "https://cipherplayground-api.onrender.com/api/"

class CaesarCipherClient:
    url = base_url + "caesar/"

    @classmethod
    def encrypt(cls, text: str, key: int, mode: int = 2) -> str:
        response = requests.post(f"{cls.url}encrypt/", json={"Text": text, "Key": key, "Mode": mode})
        return response.text

    @classmethod
    def decrypt(cls, text: str, key: int, mode: int = 2) -> str:
        response = requests.post(f"{cls.url}decrypt/", json={"Text": text, "Key": key, "Mode": mode})
        return response.text

    @classmethod
    def brute_force(cls, text: str, mode: int = 2) -> str:
        response = requests.post(f"{cls.url}bruteforce/", json={"Text": text, "Mode": mode})
        return response.text


class VigenereCipherClient:
    url = base_url + "vigenere/"

    @classmethod
    def encrypt(cls, text: str, key: str, mode: int = 2, preserve_whitespace: bool = True) -> str:
        response = requests.post(f"{cls.url}encrypt/", json={"Text": text, "Key": key, "Mode": mode, "PreserveWhitespace": preserve_whitespace})
        return response.text 

    @classmethod
    def decrypt(cls, text: str, key: str, mode: int = 2, preserve_whitespace: bool = True) -> str:
        response = requests.post(f"{cls.url}decrypt/", json={"Text": text, "Key": key, "Mode": mode, "PreserveWhitespace": preserve_whitespace})
        return response.text


class A1Z26CipherClient:
    url = base_url + "a1z26/"

    @classmethod
    def encrypt(cls, text: str, mode: int = 2, character_delimiter: str = "-", word_delimiter: str = " ") -> str:
        response = requests.post(f"{cls.url}encrypt/", json={"Text": text, "Mode": mode, "CharDelimiter": character_delimiter, "WordDelimiter": word_delimiter})
        return response.text

    @classmethod
    def decrypt(cls, text: str, mode: int = 2, character_delimiter: str = "-", word_delimiter: str = " ") -> str:
        response = requests.post(f"{cls.url}decrypt/", json={"Text": text, "Mode": mode, "CharDelimiter": character_delimiter, "WordDelimiter": word_delimiter})
        return response.text
