#!/usr/bin/python
from __future__ import print_function

import argparse
import os.path
import platform
import subprocess
import sys

SEARCHES = [
  'genetic-onepoint',
  'genetic-twopoint',
  'local-search',
  'simulated-annealing',
  'taboo-search',
]

# handling arguments

parser = argparse.ArgumentParser()

search_subparsers = parser.add_subparsers(
  title = "algorithm",
  description = "search algorithm to apply (no value for all algorithms)",
  dest = "search_algorithm",
  help = "run a specific search algorithm on all test data"
)

for s in SEARCHES:
  p = search_subparsers.add_parser(
    s
  )

  # handling algorithm-specific arguments
  if s == "simulated-annealing":
    p.add_argument("-t", "--start-temperature", help = "start temperature to use for all simulated annealing runs (default 30)", default = 30)

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

# parsing all arguments

args = parser.parse_args()

if args.search_algorithm:
  SEARCHES = [args.search_algorithm]

files = open(file, 'r').readlines()

for f in files:
  file_name = os.path.join(os.path.dirname(__file__), 'graph_color', f.strip())
  if not os.path.exists(file_name):
    print('Test file ' + file_name + ' not found.')
    continue
  for s in SEARCHES:
    print('Running ' + s + ' on ' + file_name)
    cmd_args = []
    if platform.system() != 'Windows':
      cmd_args += ['mono', '--debug']
    cmd_args += ['graph-coloring.exe', file_name, s, "120000"]

    # handling algorithm-specific parameters
    if s == "simulated-annealing":
      cmd_args += [str(args.start_temperature)]

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