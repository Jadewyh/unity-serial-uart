#include <MsTimer2.h>

#define OUTPUT_PIN 13
#define TERMINATING_CHAR 10
#define MAX_CHAR 50

char buffer_array[MAX_CHAR];
bool output;
//String str;

void setup() {
  // put your setup code here, to run once:
  pinMode(OUTPUT_PIN, OUTPUT);

  Serial.begin(9600);
  Serial.setTimeout(1000);

  MsTimer2::set(1000, writeSerial);
  MsTimer2::start();
}

void loop() {
  // put your main code here, to run repeatedly:
  if (Serial.available()) {
    for (int i = 0; i < MAX_CHAR; i++) {
      buffer_array[i] = 0;
    }
    Serial.readBytesUntil(TERMINATING_CHAR, buffer_array, MAX_CHAR);
    output = !output;
    digitalWrite(OUTPUT_PIN, output);
  }
}

void writeSerial() {
  Serial.println("ABC");
}

