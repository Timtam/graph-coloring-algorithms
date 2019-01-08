#!/usr/bin/python
from __future__ import print_function

import os.path
import platform
import subprocess
import sys

if not os.path.exists(os.path.join(os.path.dirname(__file__), 'graph_color')):
  print('Cannot find folder with test data')
  sys.exit()

if not os.path.exists(os.path.join(os.path.dirname(__file__), 'graph-coloring.exe')):
  print('Unable to find executable')
  sys.exit()

file = os.path.join(os.path.dirname(__file__), 'test_data.txt')

if not os.path.exists(file):
  print('Unable to find file with test data which should be run')
  sys.exit()

searches = [
  'local-search',
  'simulated-annealing',
  'taboo-search',
  'genetic-onepoint',
  'genetic-twopoint',
]

files = open(file, 'r').readlines()

for f in files:
  file_name = os.path.join(os.path.dirname(__file__), 'graph_color', f.strip())
  if not os.path.exists(file_name):
    print('Test file ' + file_name + ' not found.')
    continue
  for s in searches:
    print('Running ' + s + ' on ' + file_name)
    cmd_args = []
    if platform.system() != 'Windows':
      cmd_args += ['mono', '--debug']
    cmd_args += ['graph-coloring.exe', file_name, s]
    proc = subprocess.Popen(cmd_args)
    try:
      proc.communicate()

      if proc.returncode != 0:
        print("An error occurred during test, cancelling follow-ups")
        sys.exit()
    except KeyboardInterrupt:
      print('Tests interrupted')
      sys.exit()
    print('-' * 40)