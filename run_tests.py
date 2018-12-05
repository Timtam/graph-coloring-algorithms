#!/usr/bin/python
from __future__ import print_function

import os.path
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
  'taboo-search'
]

files = open(file, 'r').readlines()

for s in searches:
  for f in files:
    file_name = os.path.join(os.path.dirname(__file__), 'graph_color', f.strip())
    if not os.path.exists(file_name):
      print('Test file ' + file_name + ' not found.')
      continue
    print('Running ' + s + ' on ' + file_name)
    proc = subprocess.Popen(['mono', '--debug', 'graph-coloring.exe', file_name, s])
    proc.communicate()
    print('-' * 40)